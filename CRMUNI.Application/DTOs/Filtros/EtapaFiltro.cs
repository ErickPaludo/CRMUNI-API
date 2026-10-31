using CRMUNI.Domain.Entidades.Etapas;

namespace CRMUNI.Application.DTOs.Filtros;

public record EtapaFiltro(int? Id, int? IdFunil, EEtapa? Tipo, string? Nome);
