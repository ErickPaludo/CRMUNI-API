using System.Security.Claims;
using CRMUNI.Domain.Validacoes.Entidades.Autenticacoes;

namespace CRMUNI.UI.Api.Extensao
{
    public static class AuxiliarAutenticacao
    {
        public static Guid GetId(this ClaimsPrincipal usuario)
        {
            if (usuario.FindFirst(ClaimTypes.NameIdentifier) is null)
                throw new AutenticacaoValidacao("Token inválido.");

            return Guid.Parse(usuario.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }
    }
}
