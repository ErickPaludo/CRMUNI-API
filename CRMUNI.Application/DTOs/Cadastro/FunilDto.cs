using CRMUNI.Domain.Entidades.Funis;

namespace CRMUNI.Application.DTOs.Cadastro;

public record FunilDto(int Id, Guid IdEmpresa, EFunil Tipo, string Nome, string Descricao);
public record FunisDto(List<FunilDto> Funis);
