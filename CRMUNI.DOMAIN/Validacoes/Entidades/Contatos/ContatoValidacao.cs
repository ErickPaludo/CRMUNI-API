using CRMUNI.DOMAIN.Validacoes.Geral;

namespace CRMUNI.DOMAIN.Validacoes.Entidades.Contatos;

public class ContatoValidacao : BaseValidacao, IValidacao<ContatoValidacao>
{
    public ContatoValidacao(string erro) : base(erro)
    {
    }

    public static void Verifica(bool condicao, string mensagem)
        => VerificaExcessao<ContatoValidacao>(condicao, mensagem);
}
