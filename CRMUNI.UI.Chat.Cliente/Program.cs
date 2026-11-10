using System.Net.Http.Json;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

// Cliente console para testar o hub de chat (CRMUNI.UI.Chat) via websocket.
// Uso opcional: dotnet run -- <urlChat> <urlApi>

Console.Clear();
string urlChat = args.Length > 0 ? args[0] : Ler("URL do Chat", "http://localhost:5090");

Console.WriteLine("Entrar como: 1 - Cliente | 2 - Funcionário");
string modo = Ler("Opção", "1");

return modo == "2"
    ? await ModoFuncionario(urlChat, args.Length > 1 ? args[1] : null)
    : await ModoCliente(urlChat);

// ======================= CLIENTE =======================

static async Task<int> ModoCliente(string urlChat)
{
    int? idAtendimento = null;

    await using HubConnection conexao = CriarConexao(urlChat, token: null);
    RegistrarEventosComuns(conexao, _ => idAtendimento = null);

    if (!await Conectar(conexao)) return 1;

    try
    {
        // 1) Identificacao: empresa + celular + nome. Repete enquanto o servidor rejeitar os dados.
        RespostaContato resposta;
        while (true)
        {
            Guid idEmpresa = LerGuid("Id da empresa");
            string telefone = Ler("Seu celular com DDI e DDD (ex: 55 11 91234-5678)");
            string nome = Ler("Seu nome e sobrenome");
            if (!ConexaoAtiva(conexao)) return 1;

            try
            {
                resposta = await conexao.InvokeAsync<RespostaContato>(
                    "SolicitarAtendimento", idEmpresa, telefone, nome);
                break;
            }
            catch (HubException ex)
            {
                Escrever($"Erro: {ex.Message}", ConsoleColor.Red);
            }
        }

        Escrever(resposta.Mensagem, ConsoleColor.Cyan);

        // 2) Sem atendimento em andamento: escolha do setor (repete se invalido ou sem funcionario).
        while (resposta.Tipo == TipoResposta.SelecionarSetor)
        {
            if (!int.TryParse(Ler("Setor"), out int setor))
            {
                Escrever("Informe 1, 2 ou 3.", ConsoleColor.Yellow);
                continue;
            }

            if (!ConexaoAtiva(conexao)) return 1;

            try
            {
                resposta = await conexao.InvokeAsync<RespostaContato>("SelecionarSetor", setor);
                Escrever(resposta.Mensagem, ConsoleColor.Cyan);
            }
            catch (HubException ex)
            {
                Escrever($"Erro: {ex.Message}", ConsoleColor.Red);
            }
        }

        idAtendimento = resposta.IdAtendimento;
        MostrarHistorico(resposta.Mensagens);
    }
    catch (InvalidOperationException)
    {
        ConexaoAtiva(conexao, forcarPerdida: true);
        return 1;
    }

    Escrever("Pode digitar. ('/encerrar' ou '/sair' para terminar)", ConsoleColor.Cyan);
    await LoopDigitacao(conexao, () => idAtendimento, _ => Task.FromResult(false));
    return 0;
}

// ======================= FUNCIONÁRIO =======================

static async Task<int> ModoFuncionario(string urlChat, string? urlApiArg)
{
    string urlApi = urlApiArg ?? Ler("URL da API", "http://localhost:5078");
    string email = Ler("E-mail");
    string senha = Ler("Senha");

    string token;
    try
    {
        token = await Autenticar(urlApi, email, senha);
    }
    catch (Exception ex)
    {
        Escrever($"Falha na autenticação: {ex.Message}", ConsoleColor.Red);
        return 1;
    }

    // O funcionario e vinculado direto a cada atendimento em andamento (sem aceitar), com ou sem cliente
    // conectado. Guarda esses atendimentos e qual esta ativo (para onde vai o texto digitado).
    var sessoes = new System.Collections.Concurrent.ConcurrentDictionary<int, Atendimento>();
    int? idAtivo = null;

    await using HubConnection conexao = CriarConexao(urlChat, token);
    RegistrarEventosComuns(
        conexao,
        // Cliente saiu: o atendimento continua do funcionario e ele pode seguir escrevendo (fica gravado).
        idEncerrado =>
        {
            if (sessoes.TryGetValue(idEncerrado, out Atendimento? a))
                sessoes[idEncerrado] = a with { ClienteConectado = false };
        },
        id => $"[nº {id}{(id == idAtivo ? "" : " - outro atendimento")}] ");

    conexao.On<Sessao>("AtendimentoIniciado", s =>
    {
        sessoes[s.IdAtendimento] = new Atendimento(s.IdAtendimento, s.NomeCliente, s.TelefoneCliente, true);
        Escrever($"*** {s.NomeCliente} ({s.TelefoneCliente}) conectou no atendimento nº {s.IdAtendimento}. ***",
            ConsoleColor.Green);

        // So vira o ativo se nao houver outro. Sem await: invocar o hub de dentro do handler bloquearia o recebimento.
        if (idAtivo is null)
            _ = Ativar(s.IdAtendimento);
        else
            Escrever($"Use /trocar {s.IdAtendimento} para entrar nessa conversa.", ConsoleColor.DarkGray);
    });

    if (!await Conectar(conexao)) return 1;

    Escrever("Comandos: /atendimentos | /trocar <nº atendimento> | /transferir <idFuncionario> | /setor <1-3> | /etapa <idEtapa> | /encerrar (finaliza) | /sair.", ConsoleColor.Cyan);
    Escrever("Texto livre é enviado no atendimento ativo.", ConsoleColor.Cyan);
    await AtualizarAtendimentos();

    await LoopDigitacao(conexao, () => idAtivo, async linha =>
    {
        if (linha.Equals("/atendimentos", StringComparison.OrdinalIgnoreCase))
        {
            await AtualizarAtendimentos();
            return true;
        }

        if (linha.StartsWith("/trocar", StringComparison.OrdinalIgnoreCase))
        {
            string[] partes = linha.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (partes.Length < 2 || !int.TryParse(partes[1], out int numero) || !sessoes.ContainsKey(numero))
            {
                Escrever("Uso: /trocar <nº do atendimento>. Veja os abertos com /atendimentos.", ConsoleColor.Yellow);
                return true;
            }

            await Ativar(numero);
            return true;
        }

        return false;
    });

    return 0;

    async Task AtualizarAtendimentos()
    {
        List<Atendimento> lista = await conexao.InvokeAsync<List<Atendimento>>("ListarAtendimentos");
        sessoes.Clear();
        foreach (Atendimento a in lista) sessoes[a.IdAtendimento] = a;
        if (idAtivo is not null && !sessoes.ContainsKey(idAtivo.Value)) idAtivo = null;

        Escrever($"--- Atendimentos em andamento ({sessoes.Count}) ---", ConsoleColor.Cyan);
        foreach (Atendimento a in lista)
            Escrever($"  {(a.IdAtendimento == idAtivo ? "*" : " ")} nº {a.IdAtendimento} - {a.NomeCliente} - {a.TelefoneCliente}" +
                     $" ({(a.ClienteConectado ? "online" : "offline")})",
                ConsoleColor.White);
        if (!sessoes.IsEmpty)
            Escrever("  (* = ativo; use /trocar <nº> para mudar e ver o histórico)", ConsoleColor.DarkGray);

        // Sem conversa ativa: entra na primeira com cliente online (ou na mais antiga).
        if (idAtivo is null && lista.Count > 0)
            await Ativar((lista.FirstOrDefault(x => x.ClienteConectado) ?? lista[0]).IdAtendimento);
    }

    // Torna o atendimento o ativo e carrega as mensagens gravadas dele.
    async Task Ativar(int idAtendimento)
    {
        idAtivo = idAtendimento;
        sessoes.TryGetValue(idAtendimento, out Atendimento? a);
        Escrever($"Atendimento ativo: nº {idAtendimento} - {a?.NomeCliente}" +
                 $"{(a is { ClienteConectado: false } ? " (cliente offline: as mensagens ficam gravadas)" : "")}.",
            ConsoleColor.Green);

        try
        {
            List<Mensagem> historico = await conexao.InvokeAsync<List<Mensagem>>("RetornarMensagens", idAtendimento);
            if (historico.Count == 0)
                Escrever("(sem mensagens anteriores)", ConsoleColor.DarkGray);
            MostrarHistorico(historico);
        }
        catch (Exception ex) when (ex is HubException or InvalidOperationException)
        {
            Escrever($"Não foi possível carregar o histórico: {ex.Message}", ConsoleColor.Red);
        }
    }
}

// ======================= COMUM =======================

static HubConnection CriarConexao(string urlChat, string? token)
    => new HubConnectionBuilder()
        .WithUrl($"{urlChat.TrimEnd('/')}/hubs/chat", options =>
        {
            if (token is not null)
                options.AccessTokenProvider = () => Task.FromResult<string?>(token);
        })
        .Build();

/// <param name="aoEncerrar">Recebe o nº do atendimento encerrado.</param>
/// <param name="rotulo">Prefixo exibido antes da mensagem (ex.: numero do atendimento); null = sem prefixo.</param>
static void RegistrarEventosComuns(HubConnection conexao, Action<int> aoEncerrar, Func<int, string?>? rotulo = null)
{
    // Endpoint de retorno: mensagens gravadas do atendimento.
    conexao.On<Mensagem>("ReceberMensagem", m => EscreverMensagem(m, rotulo?.Invoke(m.IdAtendimento)));

    conexao.On<int, string>("AtendimentoEncerrado", (idAtendimento, motivo) =>
    {
        aoEncerrar(idAtendimento);
        Escrever($"*** {motivo} ***", ConsoleColor.Yellow);
    });

    conexao.Closed += erro =>
    {
        Escrever($"Conexão encerrada. {erro?.Message}", ConsoleColor.Yellow);
        return Task.CompletedTask;
    };
}

static void EscreverMensagem(Mensagem m, string? rotulo = null)
    => Escrever($"{rotulo}[{m.DataHora.ToLocalTime():dd/MM HH:mm:ss}] {m.Remetente} ({m.Tipo}): {m.Texto}",
        m.Tipo == TipoRemetente.Funcionario ? ConsoleColor.Magenta : ConsoleColor.White);

static void MostrarHistorico(List<Mensagem> mensagens)
{
    if (mensagens.Count == 0) return;
    Escrever($"--- Histórico ({mensagens.Count}) ---", ConsoleColor.DarkGray);
    foreach (Mensagem m in mensagens) EscreverMensagem(m);
    Escrever("--- fim do histórico ---", ConsoleColor.DarkGray);
}

static async Task<bool> Conectar(HubConnection conexao)
{
    try
    {
        await conexao.StartAsync();
        Escrever(string.IsNullOrEmpty(conexao.ConnectionId)
            ? "Conectado ao chat."
            : $"Conectado ao chat ({conexao.ConnectionId}).", ConsoleColor.Green);
        return true;
    }
    catch (Exception ex)
    {
        Escrever($"Não foi possível conectar ao chat: {ex.Message}", ConsoleColor.Red);
        return false;
    }
}

/// <summary>
/// Le linhas do console. Comandos especificos do modo sao tratados por <paramref name="comando"/>;
/// '/encerrar' e '/sair' sao comuns; o resto e enviado na sessao ativa.
/// </summary>
static async Task LoopDigitacao(HubConnection conexao, Func<int?> sessaoAtual, Func<string, Task<bool>> comando)
{
    while (true)
    {
        string? linha = Console.ReadLine()?.Trim();

        if (linha is null || linha.Equals("/sair", StringComparison.OrdinalIgnoreCase))
            break;

        if (linha.Length == 0)
            continue;

        if (!ConexaoAtiva(conexao))
            return;

        try
        {
            if (await comando(linha))
                continue;

            int? idSessao = sessaoAtual();

            if (linha.Equals("/encerrar", StringComparison.OrdinalIgnoreCase))
            {
                if (idSessao is not null)
                    await conexao.InvokeAsync("EncerrarAtendimento", idSessao.Value);
                continue;
            }

            if (idSessao is null)
            {
                Escrever("Nenhum atendimento ativo no momento.", ConsoleColor.Yellow);
                continue;
            }

            // Somente funcionario: /transferir <idFuncionario> | /setor <1-3> | /etapa <idEtapa>
            string[] partes = linha.Split(' ', 2, StringSplitOptions.TrimEntries);
            string? metodo = partes[0].ToLowerInvariant() switch
            {
                "/transferir" => "TransferirAtendente",
                "/setor" => "TransferirSetor",
                "/etapa" => "AlterarEtapa",
                _ => null
            };

            if (metodo is not null)
            {
                if (partes.Length == 2 && int.TryParse(partes[1], out int valor))
                    await conexao.InvokeAsync(metodo, idSessao.Value, valor);
                else
                    Escrever($"Uso: {partes[0]} <número>.", ConsoleColor.Yellow);
                continue;
            }

            // Endpoint de envio. A propria mensagem volta via "ReceberMensagem".
            await conexao.InvokeAsync("EnviarMensagem", idSessao.Value, linha);
        }
        catch (HubException ex)
        {
            Escrever($"Erro: {ex.Message}", ConsoleColor.Red);
        }
        catch (InvalidOperationException)
        {
            // Conexao caiu entre a verificacao e a chamada.
            ConexaoAtiva(conexao, forcarPerdida: true);
            return;
        }
    }

    await conexao.StopAsync();
}

/// <summary>
/// Retorna false (e avisa o usuario) se a conexao com o hub nao estiver mais ativa.
/// </summary>
static bool ConexaoAtiva(HubConnection conexao, bool forcarPerdida = false)
{
    if (!forcarPerdida && conexao.State == HubConnectionState.Connected)
        return true;

    Escrever("Conexão com o chat perdida.", ConsoleColor.Red);
    return false;
}

static async Task<string> Autenticar(string urlApi, string email, string senha)
{
    using HttpClient http = new() { BaseAddress = new Uri(urlApi) };
    HttpResponseMessage resposta =
        await http.PostAsJsonAsync("CRMUNI/Autenticacao", new { Email = email, Senha = senha });

    if (!resposta.IsSuccessStatusCode)
        throw new InvalidOperationException(
            $"{(int)resposta.StatusCode} - {await resposta.Content.ReadAsStringAsync()}");

    RespostaToken? corpo = await resposta.Content.ReadFromJsonAsync<RespostaToken>();

    return string.IsNullOrWhiteSpace(corpo?.Token)
        ? throw new InvalidOperationException("Resposta sem token.")
        : corpo.Token;
}

static Guid LerGuid(string rotulo)
{
    while (true)
    {
        if (Guid.TryParse(Ler(rotulo), out Guid valor)) return valor;
        Escrever("Informe um GUID válido (ex: 0193f1a2-...).", ConsoleColor.Yellow);
    }
}

static string Ler(string rotulo, string? padrao = null)
{
    while (true)
    {
        Console.Write(padrao is null ? $"{rotulo}: " : $"{rotulo} [{padrao}]: ");
        string? valor = Console.ReadLine()?.Trim();

        // Entrada encerrada (Ctrl+D / stdin fechado): sai em vez de repetir a pergunta para sempre.
        if (valor is null) Environment.Exit(0);

        if (!string.IsNullOrEmpty(valor)) return valor;
        if (padrao is not null) return padrao;
    }
}

static void Escrever(string texto, ConsoleColor cor)
{
    lock (Console.Out)
    {
        ConsoleColor anterior = Console.ForegroundColor;
        Console.ForegroundColor = cor;
        Console.WriteLine(texto);
        Console.ForegroundColor = anterior;
    }
}

enum TipoRemetente
{
    Cliente,
    Funcionario
}

record Mensagem(int Id, int IdAtendimento, string Remetente, TipoRemetente Tipo, string Texto, DateTime DataHora);

record Atendimento(int IdAtendimento, string NomeCliente, string TelefoneCliente, bool ClienteConectado);

record Sessao(int IdAtendimento, string NomeCliente, string TelefoneCliente, string NomeFuncionario, DateTime DataInicio);

enum TipoResposta
{
    AtendimentoEmAndamento,
    SelecionarSetor,
    AtendimentoAberto
}

record RespostaContato(TipoResposta Tipo, string Mensagem, int? IdAtendimento, List<Mensagem> Mensagens);

record RespostaToken(string Token);