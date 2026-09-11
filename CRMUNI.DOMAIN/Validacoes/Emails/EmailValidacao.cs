using CRMUNI.DOMAIN.Validacoes.Geral;

namespace CRMUNI.DOMAIN.Validacoes.Emails;

public sealed class EmailValidacao : BaseValidacao
{
    public EmailValidacao(string erro) : base(erro)
    {
    }
    public static void Verifica(bool condicao, string mensagem) => VerificaExcessao<EmailValidacao>(condicao, mensagem);
}