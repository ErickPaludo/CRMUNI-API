using CRMUNI.Application.DTOs.Autenticacao;
using CRMUNI.Application.DTOs.Cadastro;
using CRMUNI.Application.Interfaces;
using CRMUNI.Application.Interfaces.Autenticacao;

namespace CRMUNI.UI.Api.Controllers;

public static class AutenticacaoController
{
    public static void MapAutenticacao(this WebApplication app)
    {
            
        var grupo = app.MapGroup("/api/autenticacao") ;

        grupo.MapPost("/", async (
            AutenticacaoDto dto,
            IAppAutenticacaoServico autenticacaoServico) =>
        {
            var retorno = await autenticacaoServico.GeraToken(dto);
            return Results.Ok(retorno);
        });
    }
}