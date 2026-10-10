using CRMUNI.Domaidd.Validacoes.Geral;

namespace CRMUNI.Domaidd.Validacoes.ObjetosValor.Descricoes;

public sealed class DescricaoValidacao : BaseValidacao,IValidacao<DescricaoValidacao>
{
    public DescricaoValidacao(string erro) : base(erro)
    {
    }
    public static void Verifica(bool condicao, string mensagem) 
        => VerificaExcessao<DescricaoValidacao>(condicao, mensagem);
}