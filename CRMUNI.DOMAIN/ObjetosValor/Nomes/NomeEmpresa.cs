namespace CRMUNI.DOMAIN.ObjetosValor.Nomes;

public sealed record NomeEmpresa : Nome
{
    protected override bool ObrigaSegundoNome { get; } = false;
    protected override int PrimeiroCaracteresMin { get; } = 25;
    protected override int PrimeiroCaracteresMax { get; } = 180;
    private NomeEmpresa(string nome) : base(nome)
    {
    }

    public static NomeEmpresa Create(string nome) => new NomeEmpresa(nome);
}