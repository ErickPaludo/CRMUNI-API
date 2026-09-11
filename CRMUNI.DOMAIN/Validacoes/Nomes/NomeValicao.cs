using CRMUNI.DOMAIN.Validacoes.Geral;

namespace CRMUNI.DOMAIN.Validacoes.Nomes;

public class NomeValicao : BaseValidacao
{
    public NomeValicao(string erro) : base(erro)
    {
    }
    public static void Verifica(bool condicao, string mensagem) => VerificaExcessao<NomeValicao>(condicao, mensagem);
}