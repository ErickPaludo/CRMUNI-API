using CRMUNI.Application.DTOs.Cadastro;
using CRMUNI.Application.DTOs.Filtros;
using CRMUNI.Domain.Entidades.Funis;
using CRMUNI.Domain.Entidades.Setores;

namespace CRMUNI.Application.Interfaces;

public interface IEtapaServico
{
    Task Cadastra(Funil[] funils);
    Task<EtapasDto> Retorno(string idUsuario, EtapaFiltro filtros);
}
