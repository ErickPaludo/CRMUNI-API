using System.Collections.Concurrent;

namespace CRMUNI.UI.Chat.Atendimentos;

/// <summary>
/// Mantem em memoria o estado do chat: clientes escolhendo setor, fila por funcionario e sessoes ativas.
/// Mensagens nao sao persistidas; ao reiniciar o servidor, esse estado e descartado.
/// </summary>
public sealed class GerenciadorAtendimentos
{
    private readonly ConcurrentDictionary<string, DadosContato> _aguardandoSetor = new();
    private readonly ConcurrentDictionary<Guid, SolicitacaoAtendimento> _fila = new();
    private readonly ConcurrentDictionary<Guid, SessaoChat> _sessoes = new();

    public bool PossuiAtendimentoAtivo(string connectionIdCliente)
        => _fila.Values.Any(s => s.ConnectionIdCliente == connectionIdCliente)
           || _sessoes.Values.Any(s => s.ConnectionIdCliente == connectionIdCliente);

    // ---------- Escolha de setor ----------

    public void AguardarSetor(string connectionIdCliente, DadosContato dados)
        => _aguardandoSetor[connectionIdCliente] = dados;

    /// <summary>Retira (de forma atomica) os dados do cliente que esta escolhendo o setor.</summary>
    public DadosContato? RetirarAguardandoSetor(string connectionIdCliente)
        => _aguardandoSetor.TryRemove(connectionIdCliente, out DadosContato? dados) ? dados : null;

    // ---------- Fila ----------

    public SolicitacaoAtendimento Enfileirar(string connectionIdCliente, int idAtendimento,
        string idFuncionarioResponsavel, string nomeCliente, string telefoneCliente)
    {
        var solicitacao = new SolicitacaoAtendimento(
            Guid.CreateVersion7(),
            idAtendimento,
            idFuncionarioResponsavel,
            connectionIdCliente,
            nomeCliente,
            telefoneCliente,
            DateTime.UtcNow);

        _fila[solicitacao.Id] = solicitacao;
        return solicitacao;
    }

    public IReadOnlyList<SolicitacaoAtendimento> ListarFila(string idFuncionario)
        => _fila.Values
            .Where(s => s.IdFuncionarioResponsavel == idFuncionario)
            .OrderBy(s => s.DataSolicitacao)
            .ToList();

    /// <summary>
    /// Remove a solicitacao da fila de forma atomica, somente se o chamador for o funcionario responsavel.
    /// </summary>
    public SessaoChat? TryAceitar(Guid idSolicitacao, string connectionIdFuncionario, string idFuncionario,
        string nomeFuncionario)
    {
        if (!_fila.TryGetValue(idSolicitacao, out SolicitacaoAtendimento? solicitacao)
            || solicitacao.IdFuncionarioResponsavel != idFuncionario
            || !_fila.TryRemove(idSolicitacao, out solicitacao))
            return null;

        var sessao = new SessaoChat(
            Guid.CreateVersion7(),
            solicitacao.IdAtendimento,
            solicitacao.ConnectionIdCliente,
            solicitacao.NomeCliente,
            solicitacao.TelefoneCliente,
            connectionIdFuncionario,
            idFuncionario,
            nomeFuncionario,
            DateTime.UtcNow);

        _sessoes[sessao.Id] = sessao;
        return sessao;
    }

    // ---------- Sessoes ----------

    public SessaoChat? ObterSessao(Guid idSessao)
        => _sessoes.TryGetValue(idSessao, out SessaoChat? sessao) ? sessao : null;

    public SessaoChat? Encerrar(Guid idSessao)
        => _sessoes.TryRemove(idSessao, out SessaoChat? sessao) ? sessao : null;

    /// <summary>
    /// Limpa tudo que pertence a uma conexao encerrada.
    /// </summary>
    public (SolicitacaoAtendimento? solicitacaoRemovida, List<SessaoChat> sessoesEncerradas) RemoverConexao(
        string connectionId)
    {
        _aguardandoSetor.TryRemove(connectionId, out _);

        SolicitacaoAtendimento? solicitacaoRemovida = null;
        SolicitacaoAtendimento? naFila = _fila.Values.FirstOrDefault(s => s.ConnectionIdCliente == connectionId);
        if (naFila is not null && _fila.TryRemove(naFila.Id, out SolicitacaoAtendimento? removida))
            solicitacaoRemovida = removida;

        List<SessaoChat> sessoesEncerradas = new();
        foreach (SessaoChat sessao in _sessoes.Values.Where(s => s.Participa(connectionId)).ToList())
        {
            if (_sessoes.TryRemove(sessao.Id, out SessaoChat? encerrada))
                sessoesEncerradas.Add(encerrada);
        }

        return (solicitacaoRemovida, sessoesEncerradas);
    }
}
