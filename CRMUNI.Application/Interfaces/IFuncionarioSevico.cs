using CRMUNI.Application.DTOs.Cadastro;
using CRMUNI.Application.DTOs.Filtros;

namespace CRMUNI.Application.Interfaces;

public interface IFuncionarioSevico
{
    Task<FuncionarioRetornoDto> Cadastra(Guid IdUsuario, FuncionarioCadastroDto cadastroDto);
    Task<FuncionariosDto> RetornoGeral(string idUsuario,FuncionarioFiltros filtros);
    Task<FuncionarioRetornoDto> Retorno(string idUsuario);
}