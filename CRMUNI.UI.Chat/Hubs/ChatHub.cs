using System.Security.Claims;
using CRMUNI.Application.DTOs.Atendimentos;
using CRMUNI.Application.DTOs.Mensagens;
using CRMUNI.Application.Interfaces;
using CRMUNI.Domain.ObjetosValor.Nomes;
using CRMUNI.Domain.ObjetosValor.Telefones;
using CRMUNI.UI.Chat.Atendimentos;
using CRMUNI.UI.Chat.Contratos;
using CRMUNI.UI.Chat.Extensao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CRMUNI.UI.Chat.Hubs;

public sealed class ChatHub : Hub<IChatCliente>
{
    private const string MenuSetores =
        "Qual setor você deseja conversar? 1 - Comercial, 2 - Financeiro, 3 - Suporte";

    private readonly GerenciadorAtendimentos _gerenciador;
    private readonly IAtendimentoChatServico _atendimentoServico;
    private readonly IMensagemServico _mensagemServico;

    public ChatHub(GerenciadorAtendimentos gerenciador, IAtendimentoChatServico atendimentoServico,
        IMensagemServico mensagemServico)
    {
        _gerenciador = gerenciador;
        _atendimentoServico = atendimentoServico;
        _mensagemServico = mensagemServico;
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        // Somente a conexao do cliente encerra o vinculo; o funcionario segue ligado ao atendimento.
        SessaoChat? sessao = _gerenciador.RemoverConexao(Context.ConnectionId);
        if (sessao is not null)
            await Clients.User(sessao.IdFuncionario)
                .AtendimentoEncerrado(sessao.IdAtendimento, $"{sessao.NomeCliente} desconectou.");

        await base.OnDisconnectedAsync(exception);
    }

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
            return new RespostaContatoDto(ETipoResposta.SelecionarSetor, MenuSetores, null, new());
        }

        int idAtendimento = identificacao.IdAtendimento!.Value;
        List<MensagemDto> historico = await Vincular(dados, idAtendimento,
            identificacao.IdFuncionario!.Value, identificacao.NomeFuncionario!);

        return new RespostaContatoDto(
            ETipoResposta.AtendimentoEmAndamento,
            $"Você já possui o atendimento nº {idAtendimento} em andamento com {identificacao.NomeFuncionario}.",
            idAtendimento,
            historico);
    }

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

        List<MensagemDto> historico = await Vincular(dados, atendimento.IdAtendimento,
            atendimento.IdFuncionario, atendimento.NomeFuncionario);

        return new RespostaContatoDto(
            ETipoResposta.AtendimentoAberto,
            $"Atendimento nº {atendimento.IdAtendimento} aberto com {atendimento.NomeFuncionario}. Pode digitar.",
            atendimento.IdAtendimento,
            historico);
    }

    /// <summary>Atendimentos em andamento do funcionario logado, com ou sem cliente conectado.</summary>
    [Authorize]
    public async Task<List<AtendimentoDto>> ListarAtendimentos()
        => (await ListarDoFuncionarioLogado())
            .Select(a => new AtendimentoDto(a.IdAtendimento, a.NomeCliente, a.TelefoneCliente,
                _gerenciador.ObterSessao(a.IdAtendimento) is not null))
            .ToList();

    [Authorize]
    public async Task<List<MensagemDto>> RetornarMensagens(int idAtendimento)
    {
        (_, _, string idFuncionario) = await Autorizar(idAtendimento);
        return await _mensagemServico.RetornaMensagens(idAtendimento, int.Parse(idFuncionario));
    }

    /// <summary>
    /// Grava e entrega a mensagem. O funcionario pode enviar com o cliente desconectado:
    /// fica gravada e o cliente recebe no historico ao se identificar de novo.
    /// </summary>
    public async Task EnviarMensagem(int idAtendimento, string texto)
    {
        (SessaoChat? sessao, ERemetente remetente, string idFuncionario) = await Autorizar(idAtendimento);

        // Conteudo (1 a 1000 caracteres) e validado no dominio antes de gravar.
        MensagemDto mensagem = await _mensagemServico.GravaMensagem(idAtendimento, int.Parse(idFuncionario), remetente, texto);

        if (sessao is not null)
            await Clients.Client(sessao.ConnectionIdCliente).ReceberMensagem(mensagem);
        await Clients.User(idFuncionario).ReceberMensagem(mensagem);
    }

    public async Task EncerrarAtendimento(int idAtendimento)
    {
        (SessaoChat? sessao, ERemetente remetente, _) = await Autorizar(idAtendimento);
        if (sessao is null || _gerenciador.Encerrar(idAtendimento) is null)
            throw new HubException("O cliente não está conectado a este atendimento.");

        string quem = remetente == ERemetente.Funcionario ? sessao.NomeFuncionario : sessao.NomeCliente;
        string motivo = $"Atendimento encerrado por {quem}.";
        await Clients.Client(sessao.ConnectionIdCliente).AtendimentoEncerrado(idAtendimento, motivo);
        await Clients.User(sessao.IdFuncionario).AtendimentoEncerrado(idAtendimento, motivo);
    }
    
    private async Task<List<MensagemDto>> Vincular(DadosContato dados, int idAtendimento, int idFuncionario,
        string nomeFuncionario)
    {
        var sessao = new SessaoChat(idAtendimento, Context.ConnectionId, dados.Nome, dados.Telefone,
            idFuncionario.ToString(), nomeFuncionario, DateTime.UtcNow);

        if (!_gerenciador.Vincular(sessao))
            throw new HubException($"O atendimento nº {idAtendimento} já está aberto em outra conexão.");

        await Clients.User(sessao.IdFuncionario).AtendimentoIniciado(sessao.ParaDto());
        return await _mensagemServico.RetornaMensagens(idAtendimento, idFuncionario);
    }

    /// <summary>
    /// Papel de quem chama no atendimento: cliente pela conexao vinculada; funcionario se o atendimento
    /// em andamento e dele no banco (mesmo com o cliente desconectado). Sessao = null se o cliente esta offline.
    /// </summary>
    private async Task<(SessaoChat? sessao, ERemetente remetente, string idFuncionario)> Autorizar(int idAtendimento)
    {
        SessaoChat? sessao = _gerenciador.ObterSessao(idAtendimento);

        if (sessao is not null && sessao.ConnectionIdCliente == Context.ConnectionId)
            return (sessao, ERemetente.Cliente, sessao.IdFuncionario);

        if ((await ListarDoFuncionarioLogado()).Any(a => a.IdAtendimento == idAtendimento))
            return (sessao, ERemetente.Funcionario, Context.User!.GetId());

        throw new HubException("Atendimento não encontrado.");
    }

    /// <summary>Vazio se quem chama nao e funcionario (anonimo ou Usuario administrador, cujo id e Guid).</summary>
    private async Task<List<AtendimentoFuncionarioDto>> ListarDoFuncionarioLogado()
        => int.TryParse(Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int idFuncionario)
            ? await _atendimentoServico.ListarDoFuncionario(idFuncionario)
            : new();
}
