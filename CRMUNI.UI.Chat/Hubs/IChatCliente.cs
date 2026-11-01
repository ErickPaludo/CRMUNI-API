using CRMUNI.UI.Chat.Contratos;

namespace CRMUNI.UI.Chat.Hubs;

/// <summary>
/// Eventos que o servidor envia aos clientes conectados (endpoints de retorno).
/// </summary>
public interface IChatCliente
{
    /// <summary>Nova mensagem na sessao (cliente e funcionario).</summary>
    Task ReceberMensagem(MensagemChatDto mensagem);

    /// <summary>Um funcionario aceitou a solicitacao: a sessao comecou (cliente e funcionario).</summary>
    Task AtendimentoIniciado(SessaoDto sessao);

    /// <summary>A sessao foi encerrada por um dos lados ou por desconexao.</summary>
    Task AtendimentoEncerrado(Guid idSessao, string motivo);

    /// <summary>Novo cliente na fila (somente funcionarios).</summary>
    Task NovaSolicitacao(SolicitacaoDto solicitacao);

    /// <summary>Solicitacao saiu da fila: aceita por alguem ou cliente desistiu (somente funcionarios).</summary>
    Task SolicitacaoRemovida(Guid idSolicitacao);
}
