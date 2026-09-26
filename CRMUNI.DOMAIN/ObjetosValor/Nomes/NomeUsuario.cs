namespace CRMUNI.DOMAIN.ObjetosValor.Nomes;

public sealed record NomeUsuario : Nome
{
    protected override bool ObrigaSegundoNome { get; } = true;

    private NomeUsuario(string primeiroNome, string segundoNome) : base(primeiroNome, segundoNome)
    {
    }

    public static NomeUsuario Create(string primeiroNome, string segundoNome)
        => new(primeiroNome, segundoNome);
};