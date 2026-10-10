using CRMUNI.Application.DTOs.Cadastro;
using CRMUNI.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRMUNI.UI.Api.Controllers
{
    public static class EmpresaEndpoints
    {
        public static void MapEmpresa(this WebApplication app)
        {
            
            var grupo = app.MapGroup("/api/empresa") ;

            grupo.MapPost("/cadastrar", async (
                EmpresaUsuarioDTO dto,
                IEmpresaServico empresaServico) =>
            {
                await empresaServico.Cadastra(dto);
                return Results.Ok();
            });

            grupo.MapPost("/", () =>
            {
                // criar atendimento
            });
        }
    }
}
