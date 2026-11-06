using CRMUNI.Application.DTOs.Mensagens;

namespace CRMUNI.UI.Chat.Contratos;

/// <summary>
/// Retorno ao cliente em SolicitarAtendimento e SelecionarSetor.
/// Mensagens = historico gravado do atendimento (vazio enquanto o setor nao foi escolhido).
/// </summary>
public record RespostaContatoDto(ETipoResposta Tipo, string Mensagem, int? IdAtendimento, List<MensagemDto> Mensagens);
