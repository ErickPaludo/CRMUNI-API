using CRMUNI.DOMAIN.Validacoes.Geral;

namespace CRMUNI.DOMAIN.Validacoes.Entidades.Setores;

public sealed class SetorValidacao : BaseValidacao, IValidacao<SetorValidacao>
{
    public SetorValidacao(string erro) : base(erro)
    {
    }
    public static void Verifica(bool condicao, string mensagem)
        => VerificaExcessao<SetorValidacao>(condicao, mensagem);
}