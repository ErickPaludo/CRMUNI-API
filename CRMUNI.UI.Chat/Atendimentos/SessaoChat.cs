using CRMUNI.UI.Chat.Contratos;

namespace CRMUNI.UI.Chat.Atendimentos;

public sealed record SessaoChat(
    Guid Id,
    string ConnectionIdCliente,
    string NomeCliente,
    string ConnectionIdFuncionario,
    string IdFuncionario,
    string NomeFuncionario,
    DateTime DataInicio)
{
    public bool Participa(string connectionId)
        => connectionId == ConnectionIdCliente || connectionId == ConnectionIdFuncionario;

    public ETipoRemetente TipoDe(string connectionId)
        => connectionId == ConnectionIdFuncionario ? ETipoRemetente.Funcionario : ETipoRemetente.Cliente;

    public string NomeDe(string connectionId)
        => TipoDe(connectionId) == ETipoRemetente.Funcionario ? NomeFuncionario : NomeCliente;

    public string GrupoSignalR => $"sessao-{Id}";

    public SessaoDto ParaDto() => new(Id, NomeCliente, NomeFuncionario, DataInicio);
}
