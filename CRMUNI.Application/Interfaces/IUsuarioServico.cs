using CRMUNI.Application.DTOs.Cadastro;
using CRMUNI.Domain.Entidades.Empresas;

namespace CRMUNI.Application.Interfaces;

public interface IUsuarioServico
{
    Task<UsuarioRetornoDto> Cadastra(Empresa empresa,UsuarioDto usuarioDto);
    Task<UsuarioRetornoDto> Retorno(Guid idUsuario);
}