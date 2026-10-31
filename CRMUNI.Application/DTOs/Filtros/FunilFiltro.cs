using CRMUNI.Domain.Entidades.Funis;

namespace CRMUNI.Application.DTOs.Filtros;

public record FunilFiltro(int? Id, EFunil? Tipo, string? Nome);
