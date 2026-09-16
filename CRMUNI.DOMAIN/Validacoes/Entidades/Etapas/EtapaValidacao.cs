using CRMUNI.DOMAIN.Validacoes.Geral;

namespace CRMUNI.DOMAIN.Validacoes.Entidades.Etapas;

public sealed class EtapaValidacao : BaseValidacao, IValidacao<EtapaValidacao>
{
    public EtapaValidacao(string erro) : base(erro)
    {
    }

    public static void Verifica(bool condicao, string mensagem)
        => VerificaExcessao<EtapaValidacao>(condicao, mensagem);
}