using CRMUNI.Application.DTOs.Autenticacao;
using CRMUNI.Application.Interfaces.Autenticacao;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[ApiController]
[Route("CRMUNI/[controller]")]
public class AutenticacaoController : ControllerBase
{
    private readonly IAppAutenticacaoServico _appAutenticacaoServico;

    public AutenticacaoController(IAppAutenticacaoServico appAutenticacaoServico)
    {
        _appAutenticacaoServico = appAutenticacaoServico;
    }

    [HttpPost]
    public async Task<IActionResult> GeraToken(AutenticacaoDto dto)
    {
        var token = await _appAutenticacaoServico.GeraToken(dto);
        return Ok(token);
    }
}