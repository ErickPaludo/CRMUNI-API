namespace CRMUNI.DOMAIN.ObjetosValor.Documentos;

public sealed record Cnpj : Documento
{
    protected override int TamanhoNumeroDocumento { get; } = 14;

    private Cnpj(string documento) : base(documento)
    {
    }

    public static Cnpj Create(string documento) => new Cnpj(documento);
};