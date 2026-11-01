using CRMUNI.Application.DTOs.Atendimentos;
using CRMUNI.Application.Interfaces;
using CRMUNI.Domain.ObjetosValor.descricoes;
using CRMUNI.Domain.ObjetosValor.Nomes;
using CRMUNI.Domain.ObjetosValor.Telefones;
using CRMUNI.UI.Chat.Atendimentos;
using CRMUNI.UI.Chat.Contratos;
using CRMUNI.UI.Chat.Extensao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CRMUNI.UI.Chat.Hubs;

/// <summary>
/// Chat em tempo real entre cliente (anonimo) e funcionario (JWT).
/// O banco e usado para validar o token do funcionario, identificar o contato e abrir o atendimento;
/// as mensagens do chat nao sao persistidas.
/// </summary>
/// <remarks>
/// Notificacoes ao funcionario usam <c>Clients.User(id)</c>: o IUserIdProvider padrao do SignalR
/// usa a claim NameIdentifier do JWT, que e o id do funcionario.
/// </remarks>
public sealed class ChatHub : Hub<IChatCliente>
{
    private const string MenuSetores =
        "Qual setor você deseja conversar? 1 - Comercial, 2 - Financeiro, 3 - Suporte";

    private readonly GerenciadorAtendimentos _gerenciador;
    private readonly IAtendimentoChatServico _atendimentoServico;

    public ChatHub(GerenciadorAtendimentos gerenciador, IAtendimentoChatServico atendimentoServico)
    {
        _gerenciador = gerenciador;
        _atendimentoServico = atendimentoServico;
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var (solicitacaoRemovida, sessoesEncerradas) = _gerenciador.RemoverConexao(Context.ConnectionId);

        if (solicitacaoRemovida is not null)
            await Clients.User(solicitacaoRemovida.IdFuncionarioResponsavel)
                .SolicitacaoRemovida(solicitacaoRemovida.Id);

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
    /// Cliente entra em contato. Se ja houver atendimento em andamento, retorna o numero dele e
    /// encaminha ao funcionario responsavel; senao, pede a escolha do setor (<see cref="SelecionarSetor"/>).
    /// </summary>
    public async Task<RespostaContatoDto> SolicitarAtendimento(Guid idEmpresa, string telefone, string nome)
    {
        if (_gerenciador.PossuiAtendimentoAtivo(Context.ConnectionId))
            throw new HubException("Você já possui um atendimento em andamento nesta conexão.");

        // Validacoes dos objetos de valor antes de qualquer acesso ao banco.
        NomeContato nomeContato = NomeContato.Create(nome);
        Celular celular = Celular.Create(telefone);
        var dados = new DadosContato(idEmpresa, celular.Numero, nomeContato.Completo);

        IdentificacaoContatoDto identificacao = await _atendimentoServico.Identificar(idEmpresa, celular.Numero);

        if (!identificacao.PossuiAtendimentoEmAndamento)
        {
            _gerenciador.AguardarSetor(Context.ConnectionId, dados);
            return new RespostaContatoDto(ETipoResposta.SelecionarSetor, MenuSetores, null);
        }

        await Enfileirar(dados, identificacao.IdAtendimento!.Value, identificacao.IdFuncionario!.Value);

        return new RespostaContatoDto(
            ETipoResposta.AtendimentoEmAndamento,
            $"Você já possui o atendimento nº {identificacao.IdAtendimento} em andamento. " +
            $"Aguarde, {identificacao.NomeFuncionario} irá atendê-lo.",
            identificacao.IdAtendimento);
    }

    /// <summary>
    /// Cliente escolhe o setor (1 - Comercial, 2 - Financeiro, 3 - Suporte): cria o contato se necessario
    /// e abre o atendimento com o funcionario do setor com menos atendimentos.
    /// </summary>
    public async Task<RespostaContatoDto> SelecionarSetor(int setor)
    {
        DadosContato dados = _gerenciador.RetirarAguardandoSetor(Context.ConnectionId)
                             ?? throw new HubException("Chame SolicitarAtendimento antes de selecionar o setor.");

        AtendimentoAbertoDto atendimento;
        try
        {
            atendimento = await _atendimentoServico.Abrir(dados.IdEmpresa, dados.Telefone, dados.Nome, setor);
        }
        catch
        {
            // Permite tentar outro setor (ex.: setor invalido ou sem funcionario).
            _gerenciador.AguardarSetor(Context.ConnectionId, dados);
            throw;
        }

        await Enfileirar(dados, atendimento.IdAtendimento, atendimento.IdFuncionario);

        return new RespostaContatoDto(
            ETipoResposta.AtendimentoAberto,
            $"Atendimento nº {atendimento.IdAtendimento} aberto. Aguarde, {atendimento.NomeFuncionario} irá atendê-lo.",
            atendimento.IdAtendimento);
    }

    // ---------- Funcionario ----------

    [Authorize]
    public List<SolicitacaoDto> ListarFila()
        => _gerenciador.ListarFila(Context.User!.GetId()).Select(s => s.ParaDto()).ToList();

    [Authorize]
    public async Task<SessaoDto> AceitarAtendimento(Guid idSolicitacao)
    {
        SessaoChat sessao = _gerenciador.TryAceitar(
                                idSolicitacao,
                                Context.ConnectionId,
                                Context.User!.GetId(),
                                Context.User!.GetEmail())
                            ?? throw new HubException(
                                "Solicitação não encontrada ou não pertence a você.");

        await Groups.AddToGroupAsync(sessao.ConnectionIdCliente, sessao.GrupoSignalR);
        await Groups.AddToGroupAsync(sessao.ConnectionIdFuncionario, sessao.GrupoSignalR);

        SessaoDto dto = sessao.ParaDto();
        await Clients.Group(sessao.GrupoSignalR).AtendimentoIniciado(dto);

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

    private async Task Enfileirar(DadosContato dados, int idAtendimento, int idFuncionario)
    {
        string idResponsavel = idFuncionario.ToString();

        SolicitacaoAtendimento solicitacao = _gerenciador.Enfileirar(
            Context.ConnectionId, idAtendimento, idResponsavel, dados.Nome, dados.Telefone);

        await Clients.User(idResponsavel).NovaSolicitacao(solicitacao.ParaDto());
    }

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
