using System.Collections.Concurrent;

namespace CRMUNI.UI.Chat.Atendimentos;

/// <summary>
/// Mantem em memoria os clientes escolhendo setor e os clientes conectados a um atendimento.
/// As mensagens sao gravadas no banco; ao reiniciar o servidor apenas as conexoes sao perdidas.
/// </summary>
public sealed class GerenciadorAtendimentos
{
    private readonly ConcurrentDictionary<string, DadosContato> _aguardandoNome = new();
    private readonly ConcurrentDictionary<string, DadosContato> _aguardandoSetor = new();
    private readonly ConcurrentDictionary<int, SessaoChat> _sessoes = new();

    public bool PossuiAtendimentoAtivo(string connectionIdCliente)
        => _sessoes.Values.Any(s => s.ConnectionIdCliente == connectionIdCliente);

    // ---------- Informar nome ----------

    public void AguardarNome(string connectionIdCliente, DadosContato dados)
        => _aguardandoNome[connectionIdCliente] = dados;

    public DadosContato? RetirarAguardandoNome(string connectionIdCliente)
        => _aguardandoNome.TryRemove(connectionIdCliente, out DadosContato? dados) ? dados : null;

    // ---------- Escolha de setor ----------

    public void AguardarSetor(string connectionIdCliente, DadosContato dados)
        => _aguardandoSetor[connectionIdCliente] = dados;

    /// <summary>Retira (de forma atomica) os dados do cliente que esta escolhendo o setor.</summary>
    public DadosContato? RetirarAguardandoSetor(string connectionIdCliente)
        => _aguardandoSetor.TryRemove(connectionIdCliente, out DadosContato? dados) ? dados : null;

    // ---------- Sessoes ----------

    /// <summary>
    /// Liga a conexao do cliente ao atendimento; a conexao mais nova assume (recarregou a pagina, outra aba).
    /// Retorna a sessao substituida de outra conexao, para avisa-la, ou null.
    /// </summary>
    public SessaoChat? Vincular(SessaoChat sessao)
    {
        SessaoChat? anterior = null;
        _sessoes.AddOrUpdate(sessao.IdAtendimento, sessao, (_, atual) =>
        {
            anterior = atual;
            return sessao;
        });
        return anterior?.ConnectionIdCliente == sessao.ConnectionIdCliente ? null : anterior;
    }

    public SessaoChat? ObterSessao(int idAtendimento)
        => _sessoes.TryGetValue(idAtendimento, out SessaoChat? sessao) ? sessao : null;

    /// <summary>Troca o funcionario responsavel da sessao (transferencia). Null se o cliente esta offline.</summary>
    public SessaoChat? TrocarFuncionario(int idAtendimento, string idFuncionario, string nomeFuncionario)
        => _sessoes.TryGetValue(idAtendimento, out SessaoChat? atual)
            ? _sessoes[idAtendimento] = atual with { IdFuncionario = idFuncionario, NomeFuncionario = nomeFuncionario }
            : null;

    public SessaoChat? Encerrar(int idAtendimento)
        => _sessoes.TryRemove(idAtendimento, out SessaoChat? sessao) ? sessao : null;

    /// <summary>Limpa o que pertence a uma conexao de cliente encerrada; retorna a sessao dela, se houver.</summary>
    public SessaoChat? RemoverConexao(string connectionId)
    {
        _aguardandoNome.TryRemove(connectionId, out _);
        _aguardandoSetor.TryRemove(connectionId, out _);

        SessaoChat? sessao = _sessoes.Values.FirstOrDefault(s => s.ConnectionIdCliente == connectionId);
        return sessao is not null ? Encerrar(sessao.IdAtendimento) : null;
    }
}
