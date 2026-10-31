using CRMUNI.Domain.Entidades.Planos;

namespace CRMUNI.Application.DTOs.Filtros;

public record PlanoFiltro(Guid? Id, EPlanoSituacao? Situacao, string? Nome);
