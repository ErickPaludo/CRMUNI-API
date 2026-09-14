namespace CRMUNI.DOMAIN.ObjetosValor.Nomes;

public sealed record NomeFuncionario : Nome
{
    protected override bool ObrigaSegundoNome { get; } = true;

    private NomeFuncionario(string primeiroNome, string segundoNome) : base(primeiroNome, segundoNome)
    {
    }

    public static NomeFuncionario Create(string primeiroNome, string segundoNome)
        => new(primeiroNome, segundoNome);

};