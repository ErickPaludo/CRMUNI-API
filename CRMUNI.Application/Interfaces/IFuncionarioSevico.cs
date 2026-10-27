using CRMUNI.Application.DTOs.Cadastro;
using CRMUNI.Application.DTOs.Filtros;

namespace CRMUNI.Application.Interfaces;

public interface IFuncionarioSevico
{
    Task<FuncionarioRetornoDto> Cadastra(Guid IdUsuario, FuncionarioDto dto);
    Task<List<FuncionarioRetornoDto>> Retorno(string idUsuario,FuncionarioFiltros filtros);
}