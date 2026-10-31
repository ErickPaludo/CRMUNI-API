using CRMUNI.Application.DTOs.Cadastro;

namespace CRMUNI.Application.Interfaces;

public interface IPlanoServico
{
    public Task<PlanoDto> Cadatro(string idUsuario,PlanoCadastroDto dto);
}