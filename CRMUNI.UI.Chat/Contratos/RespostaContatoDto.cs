namespace CRMUNI.UI.Chat.Contratos;

/// <summary>
/// Retorno ao cliente em SolicitarAtendimento e SelecionarSetor.
/// </summary>
public record RespostaContatoDto(ETipoResposta Tipo, string Mensagem, int? IdAtendimento);
