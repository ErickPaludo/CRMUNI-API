using CRMUNI.DOMAIN.Validacoes.Geral;

namespace CRMUNI.DOMAIN.Validacoes.Entidades.Funcionarios;

public sealed class FuncionarioValidacao : BaseValidacao,IValidacao<FuncionarioValidacao>
{
    public FuncionarioValidacao(string erro) : base(erro)
    {
    }

    public static void Verifica(bool condicao, string mensagem)
        => VerificaExcessao<FuncionarioValidacao>(condicao, mensagem);
    
}