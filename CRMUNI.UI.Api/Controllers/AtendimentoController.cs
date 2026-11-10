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

    [HttpPatch("{idAtendimento:int}/etapa/{idEtapa:int}")]
    public async Task<IActionResult> AlterarEtapa(int idAtendimento, int idEtapa)
    {
        await _atendimentoServico.AlterarEtapa(User.GetId(), idAtendimento, idEtapa);
        return NoContent();
    }
}
