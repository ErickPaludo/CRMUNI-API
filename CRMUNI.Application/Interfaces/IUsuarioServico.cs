using CRMUNI.Application.DTOs.Cadastro;
using CRMUNI.Domain.Entidades.Empresas;

namespace CRMUNI.Application.Interfaces;

public interface IUsuarioServico
{
    Task Cadastra(Empresa empresa,UsuarioDTO usuarioDto);
}