using System.Collections.Concurrent;

namespace CRMUNI.UI.Chat.Atendimentos;

/// <summary>
/// Mantem em memoria a fila de clientes aguardando e as sessoes de chat ativas.
/// Nada e persistido: ao reiniciar o servidor, fila e sessoes sao descartadas.
/// </summary>
public sealed class GerenciadorAtendimentos
{
    private readonly ConcurrentDictionary<Guid, SolicitacaoAtendimento> _fila = new();
    private readonly ConcurrentDictionary<Guid, SessaoChat> _sessoes = new();

    public SolicitacaoAtendimento? Solicitar(string connectionIdCliente, string nomeCliente)
    {
        bool jaAtivo = _fila.Values.Any(s => s.ConnectionIdCliente == connectionIdCliente)
                       || _sessoes.Values.Any(s => s.ConnectionIdCliente == connectionIdCliente);
        if (jaAtivo)
            return null;

        var solicitacao = new SolicitacaoAtendimento(
            Guid.CreateVersion7(), connectionIdCliente, nomeCliente, DateTime.UtcNow);
        _fila[solicitacao.Id] = solicitacao;
        return solicitacao;
    }

    public IReadOnlyList<SolicitacaoAtendimento> ListarFila()
        => _fila.Values.OrderBy(s => s.DataSolicitacao).ToList();

    /// <summary>
    /// Remove a solicitacao da fila de forma atomica: se dois funcionarios aceitarem ao mesmo
    /// tempo, apenas um recebe a sessao; o outro recebe null.
    /// </summary>
    public SessaoChat? TryAceitar(Guid idSolicitacao, string connectionIdFuncionario, string idFuncionario,
        string nomeFuncionario)
    {
        if (!_fila.TryRemove(idSolicitacao, out SolicitacaoAtendimento? solicitacao))
            return null;

        var sessao = new SessaoChat(
            Guid.CreateVersion7(),
            solicitacao.ConnectionIdCliente,
            solicitacao.NomeCliente,
            connectionIdFuncionario,
            idFuncionario,
            nomeFuncionario,
            DateTime.UtcNow);

        _sessoes[sessao.Id] = sessao;
        return sessao;
    }

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
