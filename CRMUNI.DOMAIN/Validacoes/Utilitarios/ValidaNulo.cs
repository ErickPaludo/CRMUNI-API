using System.Diagnostics.CodeAnalysis;
using CRMUNI.DOMAIN.Execoes;

namespace CRMUNI.DOMAIN.Validacoes.Utilitarios
{
    public static class ValidaNulo
    {
        public static void Verifica([NotNull]object? objeto, string mensagem)
        {
            if (objeto == null)
                throw new ExceptionDomain(mensagem);
        }
    }
}
