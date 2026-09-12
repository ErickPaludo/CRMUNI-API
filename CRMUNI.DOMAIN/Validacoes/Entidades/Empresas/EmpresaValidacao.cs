using CRMUNI.DOMAIN.Validacoes.Geral;

namespace CRMUNI.DOMAIN.Validacoes.Entidades.Empresas;

public class EmpresaValidacao : BaseValidacao,IValidacao<EmpresaValidacao>
{
    public EmpresaValidacao(string erro) : base(erro)
    {
    }

    public static void Verifica(bool condicao, string mensagem)
        => VerificaExcessao<EmpresaValidacao>(condicao, mensagem);
}