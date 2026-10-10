using CRMUNI.Domaidd.Validacoes.Geral;

namespace CRMUNI.Domaidd.Validacoes.ObjetosValor.Etapas.Ordens;

public sealed class OrdemValidacao : BaseValidacao,IValidacao<OrdemValidacao>
{
    public OrdemValidacao(string erro) : base(erro)
    {
    }

    public static void Verifica(bool condicao, string mensagem)
        => VerificaExcessao<OrdemValidacao>(condicao, mensagem);

}