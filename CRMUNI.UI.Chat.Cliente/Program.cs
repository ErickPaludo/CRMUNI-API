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
    Guid? idSessao = null;

    await using HubConnection conexao = CriarConexao(urlChat, token: null);
    RegistrarEventosComuns(conexao, _ => idSessao = null);

    conexao.On<Sessao>("AtendimentoIniciado", s =>
    {
        idSessao = s.Id;
        Escrever($"*** {s.NomeFuncionario} iniciou o atendimento nº {s.IdAtendimento}. Pode digitar. ***",
            ConsoleColor.Green);
    });

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
    }
    catch (InvalidOperationException)
    {
        ConexaoAtiva(conexao, forcarPerdida: true);
        return 1;
    }

    Escrever("Aguardando o funcionário... ('/sair' para desistir)", ConsoleColor.Cyan);
    await LoopDigitacao(conexao, () => idSessao, _ => Task.FromResult(false));
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

    // Um funcionario pode atender varios clientes ao mesmo tempo: guarda todas as sessoes
    // e qual esta ativa (para onde vai o texto digitado). Trocar de sessao nao desconecta ninguem.
    var sessoes = new System.Collections.Concurrent.ConcurrentDictionary<Guid, Sessao>();
    Guid? idSessaoAtiva = null;
    List<Solicitacao> fila = new();

    await using HubConnection conexao = CriarConexao(urlChat, token);
    RegistrarEventosComuns(
        conexao,
        idEncerrada =>
        {
            sessoes.TryRemove(idEncerrada, out _);
            if (idSessaoAtiva == idEncerrada)
            {
                idSessaoAtiva = null;
                if (!sessoes.IsEmpty)
                    Escrever("Atendimento ativo encerrado. Use /atendimentos e /trocar <nº>.", ConsoleColor.Yellow);
            }
        },
        idSessao => sessoes.TryGetValue(idSessao, out Sessao? s)
            ? $"[nº {s.IdAtendimento}{(idSessao == idSessaoAtiva ? "" : " - outro atendimento")}] "
            : null);

    conexao.On<Sessao>("AtendimentoIniciado", s =>
    {
        sessoes[s.Id] = s;
        idSessaoAtiva = s.Id; // o atendimento recem-aceito vira o ativo
        Escrever($"*** Atendimento nº {s.IdAtendimento} com {s.NomeCliente} ({s.TelefoneCliente}) iniciado e ativo. ***",
            ConsoleColor.Green);
    });
    conexao.On<Solicitacao>("NovaSolicitacao", s =>
        Escrever($"[fila] Atendimento nº {s.IdAtendimento}: {s.NomeCliente} - {s.TelefoneCliente} aguardando  (use /fila e /aceitar <n>)",
            ConsoleColor.Yellow));
    conexao.On<Guid>("SolicitacaoRemovida", id =>
        Escrever($"[fila] Uma solicitação saiu da fila.", ConsoleColor.DarkYellow));

    if (!await Conectar(conexao)) return 1;

    Escrever("Comandos: /fila | /aceitar <n> | /atendimentos | /trocar <nº atendimento> | /encerrar | /sair.",
        ConsoleColor.Cyan);
    Escrever("Texto livre é enviado no atendimento ativo.", ConsoleColor.Cyan);
    await MostrarFila();

    await LoopDigitacao(conexao, () => idSessaoAtiva, async linha =>
    {
        if (linha.Equals("/fila", StringComparison.OrdinalIgnoreCase))
        {
            await MostrarFila();
            return true;
        }

        if (linha.Equals("/atendimentos", StringComparison.OrdinalIgnoreCase))
        {
            MostrarAtendimentos();
            return true;
        }

        if (linha.StartsWith("/trocar", StringComparison.OrdinalIgnoreCase))
        {
            string[] partes = linha.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Sessao? destino = partes.Length >= 2 && int.TryParse(partes[1], out int numero)
                ? sessoes.Values.FirstOrDefault(x => x.IdAtendimento == numero)
                : null;

            if (destino is null)
            {
                Escrever("Uso: /trocar <nº do atendimento>. Veja os abertos com /atendimentos.", ConsoleColor.Yellow);
                return true;
            }

            idSessaoAtiva = destino.Id;
            Escrever($"Atendimento ativo: nº {destino.IdAtendimento} - {destino.NomeCliente}.", ConsoleColor.Green);
            return true;
        }

        if (linha.StartsWith("/aceitar", StringComparison.OrdinalIgnoreCase))
        {
            string[] partes = linha.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (partes.Length < 2 || !int.TryParse(partes[1], out int n) || n < 1 || n > fila.Count)
            {
                Escrever("Uso: /aceitar <número da fila>. Rode /fila para atualizar.", ConsoleColor.Yellow);
                return true;
            }

            await conexao.InvokeAsync<Sessao>("AceitarAtendimento", fila[n - 1].Id);
            fila.RemoveAt(n - 1);
            return true;
        }

        return false;
    });

    return 0;

    void MostrarAtendimentos()
    {
        Escrever($"--- Atendimentos abertos ({sessoes.Count}) ---", ConsoleColor.Cyan);
        foreach (Sessao s in sessoes.Values.OrderBy(x => x.DataInicio))
            Escrever(
                $"  {(s.Id == idSessaoAtiva ? "*" : " ")} nº {s.IdAtendimento} - {s.NomeCliente} - {s.TelefoneCliente}",
                ConsoleColor.White);
        if (!sessoes.IsEmpty)
            Escrever("  (* = ativo; use /trocar <nº> para mudar)", ConsoleColor.DarkGray);
    }

    async Task MostrarFila()
    {
        fila = await conexao.InvokeAsync<List<Solicitacao>>("ListarFila");
        Escrever($"--- Fila ({fila.Count}) ---", ConsoleColor.Cyan);
        for (int i = 0; i < fila.Count; i++)
            Escrever(
                $"  {i + 1}. Atendimento nº {fila[i].IdAtendimento} - {fila[i].NomeCliente} - {fila[i].TelefoneCliente} (desde {fila[i].DataSolicitacao.ToLocalTime():HH:mm:ss})",
                ConsoleColor.White);
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

/// <param name="aoEncerrar">Recebe o id da sessao encerrada.</param>
/// <param name="rotuloSessao">Prefixo exibido antes da mensagem (ex.: numero do atendimento); null = sem prefixo.</param>
static void RegistrarEventosComuns(HubConnection conexao, Action<Guid> aoEncerrar,
    Func<Guid, string?>? rotuloSessao = null)
{
    // Endpoint de retorno: mensagens da sessao.
    conexao.On<Mensagem>("ReceberMensagem", m =>
    {
        string? rotulo = rotuloSessao?.Invoke(m.IdSessao);
        Escrever($"{rotulo}[{m.DataHora.ToLocalTime():HH:mm:ss}] {m.Remetente} ({m.Tipo}): {m.Texto}",
            m.Tipo == TipoRemetente.Funcionario ? ConsoleColor.Magenta : ConsoleColor.White);
    });

    conexao.On<Guid, string>("AtendimentoEncerrado", (idSessao, motivo) =>
    {
        aoEncerrar(idSessao);
        Escrever($"*** {motivo} ***", ConsoleColor.Yellow);
    });

    conexao.Closed += erro =>
    {
        Escrever($"Conexão encerrada. {erro?.Message}", ConsoleColor.Yellow);
        return Task.CompletedTask;
    };
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
static async Task LoopDigitacao(HubConnection conexao, Func<Guid?> sessaoAtual, Func<string, Task<bool>> comando)
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

            Guid? idSessao = sessaoAtual();

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

record Mensagem(Guid IdSessao, string Remetente, TipoRemetente Tipo, string Texto, DateTime DataHora);

record Solicitacao(Guid Id, int IdAtendimento, string NomeCliente, string TelefoneCliente, DateTime DataSolicitacao);

record Sessao(Guid Id, int IdAtendimento, string NomeCliente, string TelefoneCliente, string NomeFuncionario,
    DateTime DataInicio);

enum TipoResposta
{
    AtendimentoEmAndamento,
    SelecionarSetor,
    AtendimentoAberto
}

record RespostaContato(TipoResposta Tipo, string Mensagem, int? IdAtendimento);

record RespostaToken(string Token);