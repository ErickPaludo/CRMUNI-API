using CRMUNI.Application.DTOs.Cadastro;
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

    public async Task<IActionResult> Criar([FromBody] FuncionarioDto dto)
    {
        var funcionario = await _funcionarioSevico.Cadastra(User.GetId(), dto);
        return Ok();
    }
}