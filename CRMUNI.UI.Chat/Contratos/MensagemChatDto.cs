namespace CRMUNI.UI.Chat.Contratos;

public record MensagemChatDto(Guid IdSessao, string Remetente, ETipoRemetente Tipo, string Texto, DateTime DataHora);
