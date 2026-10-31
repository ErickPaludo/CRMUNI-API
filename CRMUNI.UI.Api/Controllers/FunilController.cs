using CRMUNI.Application.DTOs.Filtros;
using CRMUNI.Application.Interfaces;
using CRMUNI.UI.Api.Extensao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMUNI.UI.Api.Controllers;

[ApiController]
[Authorize]
[Route("CRMUNI/[controller]")]
public class FunilController : ControllerBase
{
    private readonly IFunilServico _funilServico;

    public FunilController(IFunilServico funilServico)
    {
        _funilServico = funilServico;
    }

    [HttpGet]
    public async Task<IActionResult> Retorno([FromQuery] FunilFiltro filtro)
    {
        var funis = await _funilServico.Retorno(User.GetId(), filtro);
        return Ok(funis);
    }
}
