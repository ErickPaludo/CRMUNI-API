using CRMUNI.DOMAIN.Validacoes.Geral;

namespace CRMUNI.DOMAIN.Validacoes.ObjetosValor.Documentos;

public sealed class DocumentoValidacao : BaseValidacao,IValidacao<DocumentoValidacao>
{
    public DocumentoValidacao(string erro) : base(erro)
    {
    }
    
    public static void Verifica(bool condicao, string mensagem) 
        => VerificaExcessao<DocumentoValidacao>(condicao, mensagem);

}