using CRMUNI.Domain.ObjetosValor.Planos;
using CRMUNI.Domain.Validacoes.Geral;

namespace CRMUNI.Domain.Validacoes.ObjetosValor.Planos;

public class PrecoValidacao : BaseValidacao, IValidacao<PrecoValidacao>
{
    public PrecoValidacao(string erro) : base(erro)
    {
    }

    public static void Verifica(bool condicao, string mensagem)
        => VerificaExcessao<PrecoValidacao>(condicao, mensagem);
}