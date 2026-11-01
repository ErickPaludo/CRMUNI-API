namespace CRMUNI.UI.Chat.Atendimentos;

/// <summary>
/// Dados informados pelo cliente, guardados entre SolicitarAtendimento e SelecionarSetor.
/// </summary>
public sealed record DadosContato(Guid IdEmpresa, string Telefone, string Nome);
