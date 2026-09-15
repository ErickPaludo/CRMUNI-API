using CRMUNI.DOMAIN.Validacoes.Geral;

namespace CRMUNI.DOMAIN.Validacoes.ObjetosValor.Telefones;

public sealed class TelefoneValidacao : BaseValidacao,IValidacao<TelefoneValidacao>
{
    public TelefoneValidacao(string erro) : base(erro)
    {
    }
    public static void Verifica(bool condicao, string mensagem) 
        => VerificaExcessao<TelefoneValidacao>(condicao, mensagem);
}