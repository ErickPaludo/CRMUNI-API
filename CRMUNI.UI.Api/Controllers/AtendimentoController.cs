using CRMUNI.Application.Interfaces;
using CRMUNI.UI.Api.Extensao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMUNI.UI.Api.Controllers;

[ApiController]
[Authorize]
[Route("CRMUNI/[controller]")]
public class AtendimentoController : ControllerBase
{
    private readonly IAtendimentoServico _atendimentoServico;

    public AtendimentoController(IAtendimentoServico atendimentoServico)
    {
        _atendimentoServico = atendimentoServico;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var atendimentos = await _atendimentoServico.Listar(User.GetId());
        return Ok(atendimentos);
    }
}
