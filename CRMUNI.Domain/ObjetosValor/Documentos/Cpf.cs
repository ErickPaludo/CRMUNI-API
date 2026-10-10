namespace CRMUNI.Domaidd.ObjetosValor.Documentos;

public sealed record Cpf : Documento
{
    protected override int TamanhoNumeroDocumento { get; } = 11;

    public Cpf()
    {
    }

    public Cpf(string documento) : base(documento)
    {
    }
}