using System.Text.RegularExpressions;
using CRMUNI.Domaidd.Validacoes.ObjetosValor.Documentos;
using CRMUNI.Domaidd.Validacoes.Utilitarios;

namespace CRMUNI.Domaidd.ObjetosValor.Documentos;

public abstract record Documento
{
    public string Codigo { get; }
    protected abstract int TamanhoNumeroDocumento { get; }

    public Documento(){}
    public Documento(string documento)
    {
        ValidaNulo.Verifica(documento, DocumentoMensagens.DocumentonNulo);
        documento = Prepara(documento);
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
        DocumentoValidacao.Verifica(string.IsNullOrWhiteSpace(documento),
            DocumentoMensagens.DocumentoObrigatorio);
        DocumentoValidacao.Verifica(documento.Length != TamanhoNumeroDocumento,
            DocumentoMensagens.DocumentoCaracteresObrigatorios(TamanhoNumeroDocumento));
    }
};