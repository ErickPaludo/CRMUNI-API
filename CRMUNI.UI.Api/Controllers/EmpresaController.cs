using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRMUNI.UI.Api.Controllers
{
    public static class EmpresaEndpoints
    {
        public static void MapEmpresa(this WebApplication app)
        {
            var grupo = app.MapGroup("/api/atendimentos")
                .RequireAuthorization();

            grupo.MapGet("/", () =>
            {
                // buscar atendimentos
            });

            grupo.MapPost("/", () =>
            {
                // criar atendimento
            });
        }
    }
}
