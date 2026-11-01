using CRMUNI.UI.Chat.Contratos;

namespace CRMUNI.UI.Chat.Atendimentos;

public sealed record SolicitacaoAtendimento(
    Guid Id,
    int IdAtendimento,
    string IdFuncionarioResponsavel,
    string ConnectionIdCliente,
    string NomeCliente,
    string TelefoneCliente,
    DateTime DataSolicitacao)
{
    public SolicitacaoDto ParaDto() => new(Id, IdAtendimento, NomeCliente, TelefoneCliente, DataSolicitacao);
}
