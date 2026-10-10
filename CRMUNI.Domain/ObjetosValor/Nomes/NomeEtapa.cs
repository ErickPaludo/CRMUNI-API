namespace CRMUNI.Domaidd.ObjetosValor.Nomes;

public sealed record NomeEtapa : Nome
{
    protected override int PrimeiroCaracteresMax { get; } = 25;
    public NomeEtapa(){}
    public NomeEtapa(string primeiroNome) : base(primeiroNome)
    {
    }
};