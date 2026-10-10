using CRMUNI.Domaidd.Validacoes.Geral;

namespace CRMUNI.Domaidd.Validacoes.Entidades.Empresas;

public sealed class EmpresaValidacao : BaseValidacao,IValidacao<EmpresaValidacao>
{
    public EmpresaValidacao(string erro) : base(erro)
    {
    }

    public static void Verifica(bool condicao, string mensagem)
        => VerificaExcessao<EmpresaValidacao>(condicao, mensagem);
}