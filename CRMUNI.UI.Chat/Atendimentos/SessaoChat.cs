using CRMUNI.UI.Chat.Contratos;

namespace CRMUNI.UI.Chat.Atendimentos;

/// <summary>
/// Cliente conectado a um atendimento. O funcionario e alcancado por Clients.User(IdFuncionario),
/// entao nao depende de uma conexao especifica dele.
/// </summary>
public sealed record SessaoChat(
    int IdAtendimento,
    string ConnectionIdCliente,
    string NomeCliente,
    string TelefoneCliente,
    string IdFuncionario,
    string NomeFuncionario,
    DateTime DataInicio)
{
    public SessaoDto ParaDto()
        => new(IdAtendimento, NomeCliente, TelefoneCliente, NomeFuncionario, DataInicio);
}
