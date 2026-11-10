using CRMUNI.Domain.Execoes;
using CRMUNI.Domain.Validacoes.Geral;
using System.Security.Claims;
using CRMUNI.Application.DTOs.Atendimentos;
using CRMUNI.Application.DTOs.Cadastro;
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
    private readonly IPlanoServico _planoServico;
    private readonly IAtendimentoServico _atendimentos;

    public ChatHub(GerenciadorAtendimentos gerenciador, IAtendimentoChatServico atendimentoServico,
        IMensagemServico mensagemServico, IPlanoServico planoServico, IAtendimentoServico atendimentos)
    {
        _gerenciador = gerenciador;
        _atendimentoServico = atendimentoServico;
        _mensagemServico = mensagemServico;
        _planoServico = planoServico;
        _atendimentos = atendimentos;
    }

    /// <summary>Administrador entra no grupo da empresa e acompanha todas as conversas em tempo real.</summary>
    public override async Task OnConnectedAsync()
    {
        if (IdAdministrador is Guid idUsuario
            && await _atendimentoServico.EmpresaDoAdministrador(idUsuario) is Guid idEmpresa)
            await Groups.AddToGroupAsync(Context.ConnectionId, GrupoAdmins(idEmpresa));

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        // Somente a conexao do cliente encerra o vinculo; o funcionario segue ligado ao atendimento.
        SessaoChat? sessao = _gerenciador.RemoverConexao(Context.ConnectionId);
        if (sessao is not null)
        {
            string motivo = $"{sessao.NomeCliente} desconectou.";
            await Clients.User(sessao.IdFuncionario).AtendimentoEncerrado(sessao.IdAtendimento, motivo);
            await (await Admins(sessao.IdAtendimento)).AtendimentoEncerrado(sessao.IdAtendimento, motivo);
        }

        await base.OnDisconnectedAsync(exception);
    }

    /// <summary>
    /// 1º passo: empresa + celular. Contato ja cadastrado segue sem informar o nome
    /// (historico do atendimento em andamento ou menu de setores); contato novo recebe InformarNome.
    /// </summary>
    public async Task<RespostaContatoDto> SolicitarAtendimento(Guid idEmpresa, string telefone)
    {
        if (_gerenciador.PossuiAtendimentoAtivo(Context.ConnectionId))
            throw new HubException("Você já possui um atendimento em andamento nesta conexão.");

        Celular celular = Celular.Create(telefone);
        IdentificacaoContatoDto identificacao = await _atendimentoServico.Identificar(idEmpresa, celular.Numero);

        if (identificacao.NomeContato is null)
        {
            _gerenciador.AguardarNome(Context.ConnectionId, new DadosContato(idEmpresa, celular.Numero, ""));
            return new RespostaContatoDto(ETipoResposta.InformarNome, "Olá! Qual o seu nome?", null, new());
        }

        return await Continuar(new DadosContato(idEmpresa, celular.Numero, identificacao.NomeContato), identificacao);
    }

    public async Task<RespostaContatoDto> InformarNome(string nome)
    {
        DadosContato pendente = _gerenciador.RetirarAguardandoNome(Context.ConnectionId)
                                ?? throw new HubException("Chame SolicitarAtendimento antes de informar o nome.");

        NomeContato nomeContato;
        try
        {
            nomeContato = NomeContato.Create(nome);
        }
        catch (Exception ex) when (ex is BaseValidacao or ExceptionDomain)
        {
            // Permite corrigir o nome; a mensagem tecnica do dominio (ex.: "0.1.4 - Nome inválido.") nao vai ao cliente.
            _gerenciador.AguardarNome(Context.ConnectionId, pendente);
            throw new HubException(
                "Não consegui entender seu nome. Digite apenas seu nome, sem pontuação (ex.: Maria Silva).");
        }

        DadosContato dados = pendente with { Nome = nomeContato.Completo };
        return await Continuar(dados, await _atendimentoServico.Identificar(dados.IdEmpresa, dados.Telefone));
    }

    /// <summary>Com atendimento em andamento vincula e devolve o historico; senao pede o setor.</summary>
    private async Task<RespostaContatoDto> Continuar(DadosContato dados, IdentificacaoContatoDto identificacao)
    {
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

        // 1 = Comercial: apresenta os planos disponiveis.
        List<PlanoDto>? planos = setor == 1
            ? (await _planoServico.RetornoAtivosPorEmpresa(dados.IdEmpresa)).Planos
            : null;

        return new RespostaContatoDto(
            ETipoResposta.AtendimentoAberto,
            $"Atendimento nº {atendimento.IdAtendimento} aberto. {atendimento.NomeFuncionario} entrará em contato em instantes.",
            atendimento.IdAtendimento,
            historico,
            planos);
    }

    /// <summary>
    /// Atendimentos em andamento, com ou sem cliente conectado: do funcionario logado,
    /// ou de toda a empresa para o administrador (somente leitura).
    /// </summary>
    [Authorize]
    public async Task<List<AtendimentoDto>> ListarAtendimentos()
    {
        List<AtendimentoFuncionarioDto> lista = IdAdministrador is null
            ? await ListarDoFuncionarioLogado()
            : await _atendimentoServico.ListarDaEmpresa(await EmpresaDoAdministradorLogado());

        return lista
            .Select(a => new AtendimentoDto(a.IdAtendimento, a.NomeCliente, a.TelefoneCliente,
                _gerenciador.ObterSessao(a.IdAtendimento) is not null, a.NomeFuncionario, a.Setor))
            .ToList();
    }

    [Authorize]
    public async Task<List<MensagemDto>> RetornarMensagens(int idAtendimento)
    {
        if (IdAdministrador is not null)
            return await _mensagemServico.RetornaMensagensDaEmpresa(idAtendimento, await EmpresaDoAdministradorLogado());

        (_, _, string idFuncionario) = await Autorizar(idAtendimento);
        return await _mensagemServico.RetornaMensagens(idAtendimento, int.Parse(idFuncionario));
    }

    /// <summary>
    /// Grava e entrega a mensagem. O funcionario pode enviar com o cliente desconectado:
    /// fica gravada e o cliente recebe no historico ao se identificar de novo.
    /// </summary>
    public async Task EnviarMensagem(int idAtendimento, string texto)
    {
        if (IdAdministrador is not null)
            throw new HubException("Administradores apenas acompanham as conversas; não podem enviar mensagens.");

        (SessaoChat? sessao, ERemetente remetente, string idFuncionario) = await Autorizar(idAtendimento);

        // Conteudo (1 a 1000 caracteres) e validado no dominio antes de gravar.
        MensagemDto mensagem = await _mensagemServico.GravaMensagem(idAtendimento, int.Parse(idFuncionario), remetente, texto);

        if (sessao is not null)
            await Clients.Client(sessao.ConnectionIdCliente).ReceberMensagem(mensagem);
        await Clients.User(idFuncionario).ReceberMensagem(mensagem);
        await (await Admins(idAtendimento)).ReceberMensagem(mensagem);
    }

    /// <summary>
    /// Funcionario: finaliza o atendimento no banco (definitivo), com o cliente online ou nao.
    /// Cliente: apenas desconecta do atendimento, que segue aberto.
    /// </summary>
    public async Task EncerrarAtendimento(int idAtendimento)
    {
        (SessaoChat? sessao, ERemetente remetente, string idFuncionario) = await Autorizar(idAtendimento);

        IChatCliente admins = await Admins(idAtendimento);

        if (remetente == ERemetente.Funcionario)
        {
            await _atendimentoServico.Finalizar(idAtendimento, int.Parse(idFuncionario));
            await admins.AtendimentoEncerrado(idAtendimento, "Atendimento finalizado.");
        }

        if (sessao is null || _gerenciador.Encerrar(idAtendimento) is null)
        {
            if (remetente == ERemetente.Cliente)
                throw new HubException("O cliente não está conectado a este atendimento.");
            return;
        }

        string quem = remetente == ERemetente.Funcionario ? sessao.NomeFuncionario : sessao.NomeCliente;
        string motivo = $"Atendimento encerrado por {quem}.";
        await Clients.Client(sessao.ConnectionIdCliente).AtendimentoEncerrado(idAtendimento, motivo);
        await Clients.User(sessao.IdFuncionario).AtendimentoEncerrado(idAtendimento, motivo);
        if (remetente == ERemetente.Cliente)
            await admins.AtendimentoEncerrado(idAtendimento, motivo);
    }

    /// <summary>Passa o atendimento a um funcionario escolhido.</summary>
    [Authorize]
    public async Task TransferirAtendente(int idAtendimento, int idFuncionarioDestino)
    {
        (_, string idOrigem) = await AutorizarFuncionario(idAtendimento);
        AtendimentoAbertoDto novo = await _atendimentoServico
            .TransferirParaFuncionario(idAtendimento, int.Parse(idOrigem), idFuncionarioDestino);
        await NotificarTransferencia(idOrigem, novo);
    }

    /// <summary>Passa o atendimento ao funcionario do setor com menos atendimentos. Setor: 1, 2 ou 3.</summary>
    [Authorize]
    public async Task TransferirSetor(int idAtendimento, int setor)
    {
        (_, string idOrigem) = await AutorizarFuncionario(idAtendimento);
        AtendimentoAbertoDto novo = await _atendimentoServico
            .TransferirParaSetor(idAtendimento, int.Parse(idOrigem), setor);
        await NotificarTransferencia(idOrigem, novo);
    }

    [Authorize]
    public async Task AlterarEtapa(int idAtendimento, int idEtapa)
    {
        await AutorizarFuncionario(idAtendimento);
        await _atendimentos.AlterarEtapa(Context.User!.GetId(), idAtendimento, idEtapa);
    }

    private async Task NotificarTransferencia(string idOrigem, AtendimentoAbertoDto novo)
    {
        string idDestino = novo.IdFuncionario.ToString();
        await Clients.User(idOrigem).AtendimentoEncerrado(novo.IdAtendimento,
            $"Atendimento transferido para {novo.NomeFuncionario}.");

        // Cliente offline: o novo atendente ve o atendimento em ListarAtendimentos.
        SessaoChat? sessao = _gerenciador.TrocarFuncionario(novo.IdAtendimento, idDestino, novo.NomeFuncionario);
        if (sessao is not null)
            await Clients.User(idDestino).AtendimentoIniciado(sessao.ParaDto());

        // Administradores recarregam a lista (novo responsavel).
        await (await Admins(novo.IdAtendimento)).AtendimentoEncerrado(novo.IdAtendimento,
            $"Atendimento transferido para {novo.NomeFuncionario}.");
    }

    private async Task<(SessaoChat? sessao, string idFuncionario)> AutorizarFuncionario(int idAtendimento)
    {
        (SessaoChat? sessao, ERemetente remetente, string idFuncionario) = await Autorizar(idAtendimento);
        if (remetente != ERemetente.Funcionario)
            throw new HubException("Apenas o funcionário responsável pode executar esta ação.");
        return (sessao, idFuncionario);
    }
    
    private async Task<List<MensagemDto>> Vincular(DadosContato dados, int idAtendimento, int idFuncionario,
        string nomeFuncionario)
    {
        var sessao = new SessaoChat(idAtendimento, Context.ConnectionId, dados.Nome, dados.Telefone,
            idFuncionario.ToString(), nomeFuncionario, DateTime.UtcNow);

        SessaoChat? anterior = _gerenciador.Vincular(sessao);
        if (anterior is not null)
            await Clients.Client(anterior.ConnectionIdCliente)
                .AtendimentoEncerrado(idAtendimento, "Atendimento aberto em outra janela.");

        await Clients.User(sessao.IdFuncionario).AtendimentoIniciado(sessao.ParaDto());
        await (await Admins(idAtendimento)).AtendimentoIniciado(sessao.ParaDto());
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

    private static string GrupoAdmins(Guid idEmpresa) => $"admins:{idEmpresa}";

    /// <summary>Conexoes dos administradores da empresa do atendimento.</summary>
    private async Task<IChatCliente> Admins(int idAtendimento)
        => Clients.Group(GrupoAdmins(await _atendimentoServico.EmpresaDoAtendimento(idAtendimento)));

    /// <summary>Id do Usuario administrador logado (Guid); null para funcionario (int) ou cliente anonimo.</summary>
    private Guid? IdAdministrador
        => Guid.TryParse(Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid id) ? id : null;

    private async Task<Guid> EmpresaDoAdministradorLogado()
        => IdAdministrador is Guid id && await _atendimentoServico.EmpresaDoAdministrador(id) is Guid idEmpresa
            ? idEmpresa
            : throw new HubException("Somente atendentes e administradores podem acessar as conversas.");

    /// <summary>Vazio se quem chama nao e funcionario (anonimo ou Usuario administrador, cujo id e Guid).</summary>
    private async Task<List<AtendimentoFuncionarioDto>> ListarDoFuncionarioLogado()
        => int.TryParse(Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int idFuncionario)
            ? await _atendimentoServico.ListarDoFuncionario(idFuncionario)
            : new();
}
