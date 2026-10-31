using CRMUNI.Application.DTOs.Cadastro;
using CRMUNI.Application.DTOs.Filtros;
using CRMUNI.Domain.Entidades.Empresas;

namespace CRMUNI.Application.Interfaces;

public interface IFunilServico
{
    Task Cadastra(Empresa empresa);
    Task<FunisDto> Retorno(string idUsuario, FunilFiltro filtros);
}
