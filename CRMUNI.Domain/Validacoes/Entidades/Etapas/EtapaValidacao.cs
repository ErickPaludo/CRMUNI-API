using CRMUNI.Domaidd.Validacoes.Geral;

namespace CRMUNI.Domaidd.Validacoes.Entidades.Etapas;

public sealed class EtapaValidacao : BaseValidacao, IValidacao<EtapaValidacao>
{
    public EtapaValidacao(string erro) : base(erro)
    {
    }

    public static void Verifica(bool condicao, string mensagem)
        => VerificaExcessao<EtapaValidacao>(condicao, mensagem);
}