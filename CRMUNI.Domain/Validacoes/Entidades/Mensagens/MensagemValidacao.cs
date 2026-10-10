using CRMUNI.Domaidd.Validacoes.Geral;

namespace CRMUNI.Domaidd.Validacoes.Entidades.Mensagens;

public sealed class MensagemValidacao : BaseValidacao, IValidacao<MensagemValidacao>
{
    public MensagemValidacao(string erro) : base(erro)
    {
    }

    public static void Verifica(bool condicao, string mensagem)
        => VerificaExcessao<MensagemValidacao>(condicao, mensagem);
}