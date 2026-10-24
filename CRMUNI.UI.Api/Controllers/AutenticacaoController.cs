using CRMUNI.Application.DTOs.Autenticacao;
using CRMUNI.Application.Interfaces.Autenticacao;
using Microsoft.AspNetCore.Mvc;

namespace CRMUNI.UI.Api.Controllers;

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

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        if(refreshToken is null || string.IsNullOrEmpty(refreshToken)) 
            return Unauthorized("Refresh token invalido.");
        
        var tokenAutenticacao = await _appAutenticacaoServico.GeraRefreshToken(refreshToken);

        SetRefreshTokenCookie(tokenAutenticacao.RefreshToken);

        return Ok(tokenAutenticacao);
    }

    private void SetRefreshTokenCookie(string refreshToken)
    {
        if (string.IsNullOrEmpty(refreshToken)) return;

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true, // OBRIGATÓRIO PARA HTTPS
            SameSite = SameSiteMode.None, // OBRIGATÓRIO PARA CROSS-DOMAIN
            Expires = DateTime.UtcNow.AddDays(7),
            IsEssential = true
        };

        Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    }
}