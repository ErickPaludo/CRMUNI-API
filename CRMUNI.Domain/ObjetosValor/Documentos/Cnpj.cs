namespace CRMUNI.Domaidd.ObjetosValor.Documentos;

public sealed record Cnpj : Documento
{
    protected override int TamanhoNumeroDocumento { get; } = 14;
    
    public Cnpj(){}
    public Cnpj(string documento) : base(documento)
    {
    }
};