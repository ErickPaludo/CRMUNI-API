using CRMUNI.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CRMUNI.UI.Api.Controllers;

// TEMPORÁRIO: endpoint sem autenticação apenas para a API de testes. Remover antes de produção.
[ApiController]
[Route("CRMUNI/[controller]")]
public class TemporarioController : ControllerBase
{
    private readonly ITemporarioServico _temporarioServico;

    public TemporarioController(ITemporarioServico temporarioServico)
    {
        _temporarioServico = temporarioServico;
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAll()
    {
        await _temporarioServico.DeleteAll();
        return NoContent();
    }
}
