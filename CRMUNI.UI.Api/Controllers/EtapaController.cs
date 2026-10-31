using CRMUNI.Application.DTOs.Filtros;
using CRMUNI.Application.Interfaces;
using CRMUNI.UI.Api.Extensao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMUNI.UI.Api.Controllers;

[ApiController]
[Authorize]
[Route("CRMUNI/[controller]")]
public class EtapaController : ControllerBase
{
    private readonly IEtapaServico _etapaServico;

    public EtapaController(IEtapaServico etapaServico)
    {
        _etapaServico = etapaServico;
    }

    [HttpGet]
    public async Task<IActionResult> Retorno([FromQuery] EtapaFiltro filtro)
    {
        var etapas = await _etapaServico.Retorno(User.GetId(), filtro);
        return Ok(etapas);
    }
}
