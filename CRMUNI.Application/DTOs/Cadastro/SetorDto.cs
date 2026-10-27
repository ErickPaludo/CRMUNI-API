using CRMUNI.Domain.Entidades.Setores;

namespace CRMUNI.Application.DTOs.Cadastro;

public record SetorDto(Guid Id, Guid IdEmpresa, ESetor Tipo,string Nome,string Descricao);
