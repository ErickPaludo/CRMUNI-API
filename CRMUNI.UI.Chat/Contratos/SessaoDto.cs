namespace CRMUNI.UI.Chat.Contratos;

public record SessaoDto(
    int IdAtendimento,
    string NomeCliente,
    string TelefoneCliente,
    string NomeFuncionario,
    DateTime DataInicio);
