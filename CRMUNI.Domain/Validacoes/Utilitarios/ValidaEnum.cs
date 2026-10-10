using CRMUNI.Domaidd.Execoes;

namespace CRMUNI.Domaidd.Validacoes.Utilitarios;

public class ValidaEnum<T> where T : Enum
{
    public static void Verifica(T valor,string mensagem)
    {
        if (!Enum.IsDefined(typeof(T), valor))
            throw new ExceptionDomain(mensagem);
    }
}