using CRMUNI.Application.DTOs.Cadastro;
using CRMUNI.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[ApiController]
[Route("CRMUNI/[controller]")]
public class EmpresaController : ControllerBase
{
    private readonly IEmpresaServico _empresaServico;

    public EmpresaController(IEmpresaServico empresaServico)
    {
        _empresaServico = empresaServico;
    }

    [HttpPost("Cadastro")]
    public async Task<IActionResult> CadastroInicial(EmpresaUsuarioDTO dto)
    {
        await _empresaServico.Cadastra(dto);
        return Ok();
    }
}