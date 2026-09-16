namespace CRMUNI.DOMAIN.ObjetosValor.Nomes;

public sealed record NomeEtapa : Nome
{
    protected override int PrimeiroCaracteresMax { get; } = 25;
    protected override bool ObrigaSegundoNome { get; } = false;
    private NomeEtapa(string primeiroNome) : base(primeiroNome)
    {
    }

    public static NomeEtapa Create(string primeiroNome)
        => new(primeiroNome);
};