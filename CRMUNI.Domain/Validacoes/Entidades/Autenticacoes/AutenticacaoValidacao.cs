using CRMUNI.Domain.Validacoes.Geral;

namespace CRMUNI.Domain.Validacoes.Entidades.Autenticacoes;

public class AutenticacaoValidacao : BaseValidacao, IValidacao<AutenticacaoValidacao>
{
    public AutenticacaoValidacao(string erro) : base(erro)
    {
    }

    public static void Verifica(bool condicao, string mensagem)
        => VerificaExcessao<AutenticacaoValidacao>(condicao, mensagem);
}