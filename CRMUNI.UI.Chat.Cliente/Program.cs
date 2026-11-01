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
    string nome = Ler("Seu nome");
    Guid? idSessao = null;

    await using HubConnection conexao = CriarConexao(urlChat, token: null);
    RegistrarEventosComuns(conexao, () => idSessao = null);

    conexao.On<Sessao>("AtendimentoIniciado", s =>
    {
        idSessao = s.Id;
        Escrever($"*** {s.NomeFuncionario} iniciou seu atendimento. Pode digitar. ***", ConsoleColor.Green);
    });

    if (!await Conectar(conexao)) return 1;

    try
    {
        await conexao.InvokeAsync<Solicitacao>("SolicitarAtendimento", nome);
        Escrever("Aguardando um funcionário... ('/sair' para desistir)", ConsoleColor.Cyan);
    }
    catch (HubException ex)
    {
        Escrever($"Erro: {ex.Message}", ConsoleColor.Red);
        return 1;
    }

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

    Guid? idSessao = null;
    List<Solicitacao> fila = new();

    await using HubConnection conexao = CriarConexao(urlChat, token);
    RegistrarEventosComuns(conexao, () => idSessao = null);

    conexao.On<Sessao>("AtendimentoIniciado", s =>
    {
        idSessao = s.Id;
        Escrever($"*** Atendimento com {s.NomeCliente} iniciado. ***", ConsoleColor.Green);
    });
    conexao.On<Solicitacao>("NovaSolicitacao", s =>
        Escrever($"[fila] Novo cliente aguardando: {s.NomeCliente}  (use /fila e /aceitar <n>)", ConsoleColor.Yellow));
    conexao.On<Guid>("SolicitacaoRemovida", id =>
        Escrever($"[fila] Uma solicitação saiu da fila.", ConsoleColor.DarkYellow));

    if (!await Conectar(conexao)) return 1;

    Escrever("Comandos: /fila | /aceitar <n> | /encerrar | /sair. Texto livre envia na sessão ativa.", ConsoleColor.Cyan);
    await MostrarFila();

    await LoopDigitacao(conexao, () => idSessao, async linha =>
    {
        if (linha.Equals("/fila", StringComparison.OrdinalIgnoreCase))
        {
            await MostrarFila();
            return true;
        }

        if (linha.StartsWith("/aceitar", StringComparison.OrdinalIgnoreCase))
        {
            if (idSessao is not null)
            {
                Escrever("Encerre o atendimento atual antes de aceitar outro.", ConsoleColor.Yellow);
                return true;
            }

            string[] partes = linha.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (partes.Length < 2 || !int.TryParse(partes[1], out int n) || n < 1 || n > fila.Count)
            {
                Escrever("Uso: /aceitar <número da fila>. Rode /fila para atualizar.", ConsoleColor.Yellow);
                return true;
            }

            await conexao.InvokeAsync<Sessao>("AceitarAtendimento", fila[n - 1].Id);
            return true;
        }

        return false;
    });

    return 0;

    async Task MostrarFila()
    {
        fila = await conexao.InvokeAsync<List<Solicitacao>>("ListarFila");
        Escrever($"--- Fila ({fila.Count}) ---", ConsoleColor.Cyan);
        for (int i = 0; i < fila.Count; i++)
            Escrever($"  {i + 1}. {fila[i].NomeCliente} (desde {fila[i].DataSolicitacao.ToLocalTime():HH:mm:ss})",
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

static void RegistrarEventosComuns(HubConnection conexao, Action aoEncerrar)
{
    // Endpoint de retorno: mensagens da sessao.
    conexao.On<Mensagem>("ReceberMensagem", m =>
        Escrever($"[{m.DataHora.ToLocalTime():HH:mm:ss}] {m.Remetente} ({m.Tipo}): {m.Texto}",
            m.Tipo == TipoRemetente.Funcionario ? ConsoleColor.Magenta : ConsoleColor.White));

    conexao.On<Guid, string>("AtendimentoEncerrado", (_, motivo) =>
    {
        aoEncerrar();
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
        Escrever($"Conectado ao chat ({conexao.ConnectionId}).", ConsoleColor.Green);
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
    }

    await conexao.StopAsync();
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

static string Ler(string rotulo, string? padrao = null)
{
    while (true)
    {
        Console.Write(padrao is null ? $"{rotulo}: " : $"{rotulo} [{padrao}]: ");
        string? valor = Console.ReadLine()?.Trim();

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

record Solicitacao(Guid Id, string NomeCliente, DateTime DataSolicitacao);

record Sessao(Guid Id, string NomeCliente, string NomeFuncionario, DateTime DataInicio);

record RespostaToken(string Token);
