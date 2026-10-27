using CRMUNI.Application.DTOs.Cadastro;
using CRMUNI.Application.DTOs.Filtros;

namespace CRMUNI.Application.Interfaces;

public interface IFuncionarioSevico
{
    Task<FuncionarioRetornoDto> Cadastra(Guid IdUsuario, FuncionarioCadastroDto cadastroDto);
    Task<FuncionariosDto> Retorno(string idUsuario,FuncionarioFiltros filtros);
}