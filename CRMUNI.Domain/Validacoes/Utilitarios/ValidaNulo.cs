using System.Diagnostics.CodeAnalysis;
using CRMUNI.Domain.Execoes;

namespace CRMUNI.Domain.Validacoes.Utilitarios
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
