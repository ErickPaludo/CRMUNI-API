using CRMUNI.Application.DTOs.Cadastro;
using CRMUNI.Application.DTOs.Filtros;

namespace CRMUNI.Application.Interfaces;

public interface IPlanoServico
{
    public Task<PlanoDto> Cadatro(string idUsuario, PlanoCadastroDto dto);
    public Task<PlanoDto> Atualizar(string idUsuario,Guid idPlano,PlanoAtualizarDto dto);
    public Task<PlanosDto> Retorno(string idUsuario, PlanoFiltro filtros);
}
