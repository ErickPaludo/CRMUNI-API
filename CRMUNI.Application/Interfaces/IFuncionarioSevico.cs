using CRMUNI.Application.DTOs.Cadastro;

namespace CRMUNI.Application.Interfaces;

public interface IFuncionarioSevico
{
    Task<FuncionarioRetornoDto> Cadastra(Guid IdUsuario, FuncionarioDto dto);
}