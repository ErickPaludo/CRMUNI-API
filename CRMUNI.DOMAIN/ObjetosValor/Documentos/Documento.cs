using System.Text.RegularExpressions;
using CRMUNI.DOMAIN.Validacoes.ObjetosValor.Documentos;
using CRMUNI.DOMAIN.Validacoes.Utilitarios;

namespace CRMUNI.DOMAIN.ObjetosValor.Documentos;

public abstract record Documento
{
    protected string Codigo { get; }
    protected abstract int TamanhoNumeroDocumento { get; }

    protected Documento(string documento)
    {
        ValidaNulo.Verifica(documento, DocumentoMensagens.DocumentonNulo);
        ValidaTamanhoCaracteres(documento);
        Codigo = documento;
    }

    private string Prepara(string documento)
        => Regex.Replace(documento, @"\D", "");

    private void ValidaTamanhoCaracteres(string documento)
    {
        /*
        !IMPORTANTE!
        NÃO ESTAMOS VALIDANDO SE O DOCUMENTO É REALMENTE VALIDO, PODE SER (99) 99 9 9999-9999
        */
        documento = Prepara(documento);

        DocumentoValidacao.Verifica(string.IsNullOrWhiteSpace(documento),
            DocumentoMensagens.DocumentoObrigatorio);
        DocumentoValidacao.Verifica(documento.Length != TamanhoNumeroDocumento,
            DocumentoMensagens.DocumentoCaracteresObrigatorios(TamanhoNumeroDocumento));
    }
};