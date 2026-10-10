using CRMUNI.Domain.Validacoes.Geral;

namespace CRMUNI.Domain.Validacoes.Entidades.Contatos;

public sealed class ContatoValidacao : BaseValidacao, IValidacao<ContatoValidacao>
{
    public ContatoValidacao(string erro) : base(erro)
    {
    }

    public static void Verifica(bool condicao, string mensagem)
        => VerificaExcessao<ContatoValidacao>(condicao, mensagem);
}
