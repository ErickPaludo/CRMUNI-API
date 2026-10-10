using CRMUNI.Domain.Validacoes.Geral;

namespace CRMUNI.Domain.Validacoes.Entidades.Empresas;

public sealed class EmpresaValidacao : BaseValidacao,IValidacao<EmpresaValidacao>
{
    public EmpresaValidacao(string erro) : base(erro)
    {
    }

    public static void Verifica(bool condicao, string mensagem)
        => VerificaExcessao<EmpresaValidacao>(condicao, mensagem);
}