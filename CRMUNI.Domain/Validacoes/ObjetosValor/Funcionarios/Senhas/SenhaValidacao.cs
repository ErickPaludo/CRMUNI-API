using CRMUNI.Domain.Validacoes.Geral;

namespace CRMUNI.Domain.Validacoes.ObjetosValor.Funcionarios.Senhas;

public sealed class SenhaValidacao : BaseValidacao, IValidacao<SenhaValidacao>
{
    public SenhaValidacao(string erro) : base(erro)
    {
    }

    public static void Verifica(bool condicao, string mensagem)
        => VerificaExcessao<SenhaValidacao>(condicao, mensagem);

}