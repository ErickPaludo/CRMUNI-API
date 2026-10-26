using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMUNI.UI.Api.Controllers;
[ApiController]
[Authorize]
[Route("CRMUNI/[controller]")]
public class FuncionarioController : ControllerBase
{
    public async Task<IActionResult> Criar()
    {
        return Ok();
    }
}