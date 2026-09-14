using CRMUNI.DOMAIN.Validacoes.Geral;

namespace CRMUNI.DOMAIN.Validacoes.ObjetosValor.Funcionarios.Senhas;

public sealed class SenhaValidacao : BaseValidacao, IValidacao<SenhaValidacao>
{
    public SenhaValidacao(string erro) : base(erro)
    {
    }

    public static void Verifica(bool condicao, string mensagem)
        => VerificaExcessao<SenhaValidacao>(condicao, mensagem);

}