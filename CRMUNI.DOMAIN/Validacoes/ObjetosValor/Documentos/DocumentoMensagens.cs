namespace CRMUNI.DOMAIN.Validacoes.ObjetosValor.Documentos;

public static class DocumentoMensagens
{
    /// <summary>
    /// Codigo de mensagem 0.3.x
    /// Valores de x.x.0 até x.x.10 são reservados para codigos "COMUNS"
    /// </summary>
    public const string DocumentoneNulo = "0.3.0 - Documento não pode ser nullo";

    public const string DocumentoObrigatorio = "0.3.1 - Documento deve ser informado.";

    public static string DocumentoCaracteresObrigatorios(int DocumentoCaracteresMax) =>
        $"0.3.3 - Documento deve possuir no máximo {DocumentoCaracteresMax} dígitos.";

    public const string DocumentoInvalido = "0.3.4 - Documento informado está inválido.";
}