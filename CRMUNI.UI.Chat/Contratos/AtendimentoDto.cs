namespace CRMUNI.UI.Chat.Contratos;

/// <summary>Atendimento em andamento do funcionario logado. ClienteConectado = cliente esta no chat agora.</summary>
public record AtendimentoDto(int IdAtendimento, string NomeCliente, string TelefoneCliente, bool ClienteConectado);
