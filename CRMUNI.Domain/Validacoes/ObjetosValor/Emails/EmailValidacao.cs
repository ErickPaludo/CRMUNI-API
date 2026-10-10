using CRMUNI.Domaidd.Validacoes.Geral;

namespace CRMUNI.Domaidd.Validacoes.ObjetosValor.Emails;

public sealed class EmailValidacao : BaseValidacao,IValidacao<EmailValidacao>
{
    public EmailValidacao(string erro) : base(erro)
    {
    }
    public static void Verifica(bool condicao, string mensagem) 
        => VerificaExcessao<EmailValidacao>(condicao, mensagem);
}