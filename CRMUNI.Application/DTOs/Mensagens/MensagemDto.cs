namespace CRMUNI.Application.DTOs.Mensagens;

/// <summary>Mensagem gravada de um atendimento. Remetente = nome de quem enviou.</summary>
public record MensagemDto(int Id, int IdAtendimento, string Remetente, ERemetente Tipo, string Texto, DateTime DataHora);
