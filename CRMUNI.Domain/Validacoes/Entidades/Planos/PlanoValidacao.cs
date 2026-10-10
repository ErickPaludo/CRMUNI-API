using CRMUNI.Domain.Validacoes.Geral;

namespace CRMUNI.Domain.Validacoes.Entidades.Planos;

public sealed class PlanoValidacao : BaseValidacao, IValidacao<PlanoValidacao>
{
    public PlanoValidacao(string erro) : base(erro)
    {
    }

    public static void Verifica(bool condicao, string mensagem)
        => VerificaExcessao<PlanoValidacao>(condicao, mensagem);
}