using CRMUNI.DOMAIN.Validacoes.Geral;

namespace CRMUNI.DOMAIN.Validacoes.ObjetosValor.Descricoes;

public class DescricaoValidacao : BaseValidacao,IValidacao<DescricaoValidacao>
{
    public DescricaoValidacao(string erro) : base(erro)
    {
    }
    public static void Verifica(bool condicao, string mensagem) 
        => VerificaExcessao<DescricaoValidacao>(condicao, mensagem);
}