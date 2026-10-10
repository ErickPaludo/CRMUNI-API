using System.Diagnostics.CodeAnalysis;
using CRMUNI.Domaidd.Execoes;

namespace CRMUNI.Domaidd.Validacoes.Utilitarios
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
