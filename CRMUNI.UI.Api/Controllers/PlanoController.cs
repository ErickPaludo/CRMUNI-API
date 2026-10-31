using CRMUNI.Application.DTOs.Cadastro;
using CRMUNI.Application.Interfaces;
using CRMUNI.Domain.Repositorios;
using CRMUNI.UI.Api.Extensao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMUNI.UI.Api.Controllers;

[ApiController]
[Authorize]
[Route("CRMUNI/[controller]")]
public class PlanoController : ControllerBase
{
    private readonly IPlanoServico _planoServico;

    public PlanoController(IPlanoServico planoServico)
        => _planoServico = planoServico;


    [HttpPost]
    public async Task<IActionResult> CadastrarPlano([FromBody] PlanoCadastroDto dto)
    {
        var ret = await _planoServico.Cadatro(User.GetId(), dto);
        return Created(string.Empty,ret);
    }
}