using CRMUNI.Domaidd.Validacoes.Geral;

namespace CRMUNI.Domaidd.Validacoes.Entidades.Contatos;

public sealed class ContatoValidacao : BaseValidacao, IValidacao<ContatoValidacao>
{
    public ContatoValidacao(string erro) : base(erro)
    {
    }

    public static void Verifica(bool condicao, string mensagem)
        => VerificaExcessao<ContatoValidacao>(condicao, mensagem);
}
