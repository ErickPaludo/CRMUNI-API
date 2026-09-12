using CRMUNI.DOMAIN.Validacoes.Geral;

namespace CRMUNI.DOMAIN.Validacoes.Telefones;

public class TelefoneValidacao : BaseValidacao
{
    public TelefoneValidacao(string erro) : base(erro)
    {
    }
    public static void Verifica(bool condicao, string mensagem) => VerificaExcessao<TelefoneValidacao>(condicao, mensagem);
}