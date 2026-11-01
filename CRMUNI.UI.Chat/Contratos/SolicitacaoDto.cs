namespace CRMUNI.UI.Chat.Contratos;

public record SolicitacaoDto(
    Guid Id,
    int IdAtendimento,
    string NomeCliente,
    string TelefoneCliente,
    DateTime DataSolicitacao);
