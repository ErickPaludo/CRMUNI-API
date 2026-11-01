using CRMUNI.Domain.ObjetosValor.descricoes;
using CRMUNI.UI.Chat.Atendimentos;
using CRMUNI.UI.Chat.Contratos;
using CRMUNI.UI.Chat.Extensao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CRMUNI.UI.Chat.Hubs;

/// <summary>
/// Chat em tempo real entre cliente (anonimo) e funcionario (JWT).
/// Nao ha persistencia: o unico acesso ao banco e a validacao do token do funcionario.
/// </summary>
public sealed class ChatHub : Hub<IChatCliente>
{
    private const string GrupoFuncionarios = "funcionarios";
    private const int TamanhoMaximoNome = 100;

    private readonly GerenciadorAtendimentos _gerenciador;

    public ChatHub(GerenciadorAtendimentos gerenciador)
    {
        _gerenciador = gerenciador;
    }

    public override async Task OnConnectedAsync()
    {
        if (EhFuncionario)
            await Groups.AddToGroupAsync(Context.ConnectionId, GrupoFuncionarios);

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var (solicitacaoRemovida, sessoesEncerradas) = _gerenciador.RemoverConexao(Context.ConnectionId);

        if (solicitacaoRemovida is not null)
            await Clients.Group(GrupoFuncionarios).SolicitacaoRemovida(solicitacaoRemovida.Id);

        foreach (SessaoChat sessao in sessoesEncerradas)
        {
            string quem = sessao.NomeDe(Context.ConnectionId);
            await Clients.Group(sessao.GrupoSignalR).AtendimentoEncerrado(sessao.Id, $"{quem} desconectou.");
            await RemoverDoGrupo(sessao);
        }

        await base.OnDisconnectedAsync(exception);
    }

    // ---------- Cliente ----------

    /// <summary>
    /// Cliente entra em contato: fica na fila ate um funcionario aceitar.
    /// </summary>
    public async Task<SolicitacaoDto> SolicitarAtendimento(string nome)
    {
        nome = nome?.Trim() ?? string.Empty;
        if (nome.Length is 0 or > TamanhoMaximoNome)
            throw new HubException($"Informe um nome com 1 a {TamanhoMaximoNome} caracteres.");

        SolicitacaoAtendimento solicitacao = _gerenciador.Solicitar(Context.ConnectionId, nome)
                                             ?? throw new HubException("Você já possui um atendimento em andamento.");

        SolicitacaoDto dto = solicitacao.ParaDto();
        await Clients.Group(GrupoFuncionarios).NovaSolicitacao(dto);
        return dto;
    }

    // ---------- Funcionario ----------

    [Authorize]
    public List<SolicitacaoDto> ListarFila()
        => _gerenciador.ListarFila().Select(s => s.ParaDto()).ToList();

    [Authorize]
    public async Task<SessaoDto> AceitarAtendimento(Guid idSolicitacao)
    {
        SessaoChat sessao = _gerenciador.TryAceitar(
                                idSolicitacao,
                                Context.ConnectionId,
                                Context.User!.GetId(),
                                Context.User!.GetEmail())
                            ?? throw new HubException("Solicitação não encontrada ou já atendida por outro funcionário.");

        await Groups.AddToGroupAsync(sessao.ConnectionIdCliente, sessao.GrupoSignalR);
        await Groups.AddToGroupAsync(sessao.ConnectionIdFuncionario, sessao.GrupoSignalR);

        SessaoDto dto = sessao.ParaDto();
        await Clients.Group(sessao.GrupoSignalR).AtendimentoIniciado(dto);
        await Clients.OthersInGroup(GrupoFuncionarios).SolicitacaoRemovida(idSolicitacao);

        return dto;
    }

    // ---------- Ambos ----------

    /// <summary>
    /// Endpoint de envio: repassa a mensagem para os participantes da sessao
    /// pelo endpoint de retorno <see cref="IChatCliente.ReceberMensagem"/>.
    /// </summary>
    public async Task EnviarMensagem(Guid idSessao, string texto)
    {
        SessaoChat sessao = ObterSessaoDaConexao(idSessao);

        // Reaproveita a validacao do objeto de valor (1 a 1000 caracteres), sem persistir.
        Conteudo conteudo = new Conteudo(texto);

        var mensagem = new MensagemChatDto(
            sessao.Id,
            sessao.NomeDe(Context.ConnectionId),
            sessao.TipoDe(Context.ConnectionId),
            conteudo.Texto,
            DateTime.UtcNow);

        await Clients.Group(sessao.GrupoSignalR).ReceberMensagem(mensagem);
    }

    public async Task EncerrarAtendimento(Guid idSessao)
    {
        SessaoChat sessao = ObterSessaoDaConexao(idSessao);
        if (_gerenciador.Encerrar(sessao.Id) is null)
            return;

        string quem = sessao.NomeDe(Context.ConnectionId);
        await Clients.Group(sessao.GrupoSignalR).AtendimentoEncerrado(sessao.Id, $"Atendimento encerrado por {quem}.");
        await RemoverDoGrupo(sessao);
    }

    private bool EhFuncionario => Context.User?.Identity?.IsAuthenticated == true;

    private SessaoChat ObterSessaoDaConexao(Guid idSessao)
    {
        SessaoChat? sessao = _gerenciador.ObterSessao(idSessao);
        if (sessao is null || !sessao.Participa(Context.ConnectionId))
            throw new HubException("Sessão não encontrada.");

        return sessao;
    }

    private async Task RemoverDoGrupo(SessaoChat sessao)
    {
        await Groups.RemoveFromGroupAsync(sessao.ConnectionIdCliente, sessao.GrupoSignalR);
        await Groups.RemoveFromGroupAsync(sessao.ConnectionIdFuncionario, sessao.GrupoSignalR);
    }
}
