namespace CRMUNI.UI.Chat.Contratos;

public record SessaoDto(
    Guid Id,
    int IdAtendimento,
    string NomeCliente,
    string TelefoneCliente,
    string NomeFuncionario,
    DateTime DataInicio);
