namespace CRMUNI.Domain.ObjetosValor.Nomes;

public sealed record NomePlano : Nome
{
    protected override int PrimeiroCaracteresMax { get; } = 25;

    public NomePlano()
    {
    }

    public NomePlano(string primeiroNome) : base(primeiroNome)
    {
    }
}