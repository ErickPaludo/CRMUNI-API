namespace CRMUNI.DOMAIN.ObjetosValor.Nomes;

public sealed record NomeEmpresa : Nome
{
    protected override int PrimeiroCaracteresMin { get; } = 25;
    public NomeEmpresa(string nome) : base(nome)
    {
    }
}