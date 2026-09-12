namespace CRMUNI.DOMAIN.ObjetosValor.Nomes;

public sealed record NomeSetor : Nome
{
    protected override bool ObrigaSegundoNome { get; } = false;
    protected override int PrimeiroCaracteresMin { get; } = 3;

    private NomeSetor(string nome) : base(nome)
    {
    }

    public static NomeSetor Create(string nome) => new(nome);
}