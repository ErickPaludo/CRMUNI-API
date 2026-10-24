using CRMUNI.Application.DTOs.Cadastro;

namespace CRMUNI.Application.Interfaces;

public interface IEmpresaServico
{
    Task<EmpresaRetornoDto> Cadastra(EmpresaUsuarioDto empresaUsuarioDto);
}