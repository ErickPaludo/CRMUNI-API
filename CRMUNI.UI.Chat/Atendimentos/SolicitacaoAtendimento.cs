using CRMUNI.UI.Chat.Contratos;

namespace CRMUNI.UI.Chat.Atendimentos;

public sealed record SolicitacaoAtendimento(
    Guid Id,
    string ConnectionIdCliente,
    string NomeCliente,
    DateTime DataSolicitacao)
{
    public SolicitacaoDto ParaDto() => new(Id, NomeCliente, DataSolicitacao);
}
