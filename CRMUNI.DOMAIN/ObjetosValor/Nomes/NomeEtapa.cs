namespace CRMUNI.DOMAIN.ObjetosValor.Nomes;

public sealed record NomeEtapa : Nome
{
    protected override int PrimeiroCaracteresMax { get; } = 25;
    public NomeEtapa(string primeiroNome) : base(primeiroNome)
    {
    }
};