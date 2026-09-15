using CRMUNI.DOMAIN.Validacoes.Geral;

namespace CRMUNI.DOMAIN.Validacoes.ObjetosValor.Nomes;

public sealed class NomeValicao : BaseValidacao,IValidacao<NomeValicao>
{
    public NomeValicao(string erro) : base(erro)
    {
    }
    public static void Verifica(bool condicao, string mensagem) 
        => VerificaExcessao<NomeValicao>(condicao, mensagem);
}