namespace CRMUNI.Application.DTOs.Atendimentos;

/// <summary>
/// Resultado da identificacao do cliente pelo celular. IdAtendimento preenchido = atendimento em andamento.
/// </summary>
public record IdentificacaoContatoDto(
    int? IdContato,
    int? IdAtendimento,
    int? IdFuncionario,
    string? NomeFuncionario)
{
    public bool PossuiAtendimentoEmAndamento => IdAtendimento.HasValue;
}
