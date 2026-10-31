using CRMUNI.Domain.Entidades.Empresas;

namespace CRMUNI.Application.DTOs.Cadastro;

public record PlanoDto(Guid Id,Guid IdEmpresa,string Nome,string Descricao);
public record PlanosDto(List<PlanoDto> Planos);
public record PlanoCadastroDto(string Nome,string Descricao);