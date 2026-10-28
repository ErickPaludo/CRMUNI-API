using CRMUNI.Application.DTOs.Filtros;
using CRMUNI.Application.Interfaces;
using CRMUNI.UI.Api.Extensao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMUNI.UI.Api.Controllers;

[ApiController]
[Authorize]
[Route("CRMUNI/[controller]")]
public class SetorController : ControllerBase
{
    private readonly ISetorServico _setorServico;

    public SetorController(ISetorServico setorServico)
    {
        _setorServico = setorServico;
    }

    [HttpGet]
    public async Task<IActionResult> Retorno([FromQuery] SetorFiltro setor)
    {
        var setores = await _setorServico.Retorno(User.GetId(), setor);
        return Ok(setores);
    }
}