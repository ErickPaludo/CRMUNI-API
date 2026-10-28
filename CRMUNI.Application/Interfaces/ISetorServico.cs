using CRMUNI.Application.DTOs.Cadastro;
using CRMUNI.Application.DTOs.Filtros;
using CRMUNI.Domain.Entidades.Empresas;

namespace CRMUNI.Application.Interfaces;

public interface ISetorServico
{
    Task Cadastra(Empresa empresa);
    Task<SetoresDto> Retorno(string idUsuario,SetorFiltro filtros);
}