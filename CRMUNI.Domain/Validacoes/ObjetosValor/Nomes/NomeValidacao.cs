using CRMUNI.Domaidd.Validacoes.Geral;

namespace CRMUNI.Domaidd.Validacoes.ObjetosValor.Nomes;

public sealed class NomeValidacao : BaseValidacao,IValidacao<NomeValidacao>
{
    public NomeValidacao(string erro) : base(erro)
    {
    }
    public static void Verifica(bool condicao, string mensagem) 
        => VerificaExcessao<NomeValidacao>(condicao, mensagem);
}