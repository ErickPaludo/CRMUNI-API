using CRMUNI.Domain.Entidades.Empresas;
using CRMUNI.Domain.Entidades.Planos;

namespace CRMUNI.Application.DTOs.Cadastro;

public record PlanoDto(Guid Id, Guid IdEmpresa, string Nome, string Descricao, decimal Preco, EPlanoSituacao Situacao);
public record PlanosDto(List<PlanoDto> Planos);
public record PlanoCadastroDto(string Nome, string Descricao, decimal Preco);