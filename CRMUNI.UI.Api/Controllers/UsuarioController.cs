using CRMUNI.Application.DTOs.Filtros;
using CRMUNI.Application.Interfaces;
using CRMUNI.Domain.Repositorios;
using CRMUNI.UI.Api.Extensao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMUNI.UI.Api.Controllers;

[ApiController]
[Authorize]
[Route("CRMUNI/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioServico _usuarioServico;

    public UsuarioController(IUsuarioServico usuarioServico)
    {
        _usuarioServico = usuarioServico;
    }

    [HttpGet]
    public async Task<IActionResult> RetornoUsuario()
    {
        // Id de funcionário é int; só o admin (Usuario) tem Guid.
        if (!Guid.TryParse(User.GetId(), out Guid idUsuario))
            return Forbid();

        var usuario = await _usuarioServico.Retorno(idUsuario);
        return Ok(usuario);
    }
}