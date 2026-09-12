using CRMUNI.DOMAIN.Validacoes.Geral;

namespace CRMUNI.DOMAIN.Validacoes.ObjetosValor.Documentos;

public class DocumentoValidacao : BaseValidacao
{
    public DocumentoValidacao(string erro) : base(erro)
    {
    }
    
    public static void Verifica(bool condicao, string mensagem) => VerificaExcessao<DocumentoValidacao>(condicao, mensagem);

}