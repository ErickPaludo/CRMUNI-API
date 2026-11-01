using System.Security.Claims;
using CRMUNI.Domain.Validacoes.Entidades.Autenticacoes;

namespace CRMUNI.UI.Chat.Extensao;

public static class AuxiliarAutenticacao
{
    public static string GetId(this ClaimsPrincipal usuario)
        => usuario.FindFirst(ClaimTypes.NameIdentifier)?.Value
           ?? throw new AutenticacaoValidacao("Token inválido.");

    public static string GetEmail(this ClaimsPrincipal usuario)
        => usuario.FindFirst(ClaimTypes.Email)?.Value
           ?? throw new AutenticacaoValidacao("Token inválido.");
}
