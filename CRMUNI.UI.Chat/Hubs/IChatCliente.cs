using CRMUNI.Application.DTOs.Mensagens;
using CRMUNI.UI.Chat.Contratos;

namespace CRMUNI.UI.Chat.Hubs;

/// <summary>
/// Eventos que o servidor envia aos clientes conectados (endpoints de retorno).
/// </summary>
public interface IChatCliente
{
    /// <summary>Nova mensagem gravada no atendimento (cliente e funcionario).</summary>
    Task ReceberMensagem(MensagemDto mensagem);

    /// <summary>Cliente conectou ao atendimento do funcionario (somente o funcionario responsavel).</summary>
    Task AtendimentoIniciado(SessaoDto sessao);

    /// <summary>A conexao do cliente com o atendimento foi encerrada por um dos lados ou por desconexao.</summary>
    Task AtendimentoEncerrado(int idAtendimento, string motivo);
}
