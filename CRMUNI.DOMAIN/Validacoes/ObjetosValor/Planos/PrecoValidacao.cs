using CRMUNI.DOMAIN.ObjetosValor.Planos;
using CRMUNI.DOMAIN.Validacoes.Geral;

namespace CRMUNI.DOMAIN.Validacoes.ObjetosValor.Planos;

public class PrecoValidacao : BaseValidacao, IValidacao<PrecoValidacao>
{
    public PrecoValidacao(string erro) : base(erro)
    {
    }

    public static void Verifica(bool condicao, string mensagem)
        => VerificaExcessao<PrecoValidacao>(condicao, mensagem);
}