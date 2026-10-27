using CRMUNI.Application.DTOs.Cadastro;
using CRMUNI.Application.DTOs.Filtros;
using CRMUNI.Application.Interfaces;
using CRMUNI.UI.Api.Extensao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMUNI.UI.Api.Controllers;

[ApiController]
[Authorize]
[Route("CRMUNI/[controller]")]
public class FuncionarioController : ControllerBase
{
    private readonly IFuncionarioSevico _funcionarioSevico;

    public FuncionarioController(IFuncionarioSevico funcionarioSevico)
    {
        _funcionarioSevico = funcionarioSevico;
    }
    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] FuncionarioDto dto)
    {
        var funcionario = await _funcionarioSevico.Cadastra(User.GetId(), dto);
        return Ok(funcionario);
    } 
    [HttpGet]
    public async Task<IActionResult> Retorno([FromQuery] FuncionarioFiltros filtros)
    {
        var funcionario = await _funcionarioSevico.Retorno(User.GetId().ToString(), filtros);
        return Ok(funcionario);
    }
}