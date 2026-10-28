using System.Security.Claims;
using CRMUNI.Domain.Validacoes.Entidades.Autenticacoes;

namespace CRMUNI.UI.Api.Extensao
{
    public static class AuxiliarAutenticacao
    {
        public static string GetId(this ClaimsPrincipal usuario)
        {
            if (usuario.FindFirst(ClaimTypes.NameIdentifier) is null)
                throw new AutenticacaoValidacao("Token inválido.");

            return usuario.FindFirst(ClaimTypes.NameIdentifier)!.Value.ToString();
        }
    }
}
