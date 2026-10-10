using CRMUNI.Application.DTOs.Cadastro;

namespace CRMUNI.Application.Interfaces;

public interface IEmpresaServico
{
    Task Cadastra(EmpresaUsuarioDTO empresaUsuarioDTO);
}