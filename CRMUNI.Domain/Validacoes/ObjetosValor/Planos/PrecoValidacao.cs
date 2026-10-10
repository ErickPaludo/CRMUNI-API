using CRMUNI.Domaidd.Validacoes.Geral;
using CRMUNI.Domaidd.ObjetosValor.Planos;

namespace CRMUNI.Domaidd.Validacoes.ObjetosValor.Planos;

public class PrecoValidacao : BaseValidacao, IValidacao<PrecoValidacao>
{
    public PrecoValidacao(string erro) : base(erro)
    {
    }

    public static void Verifica(bool condicao, string mensagem)
        => VerificaExcessao<PrecoValidacao>(condicao, mensagem);
}