namespace CRMUNI.DOMAIN.ObjetosValor.Nomes;

public sealed record NomeContato : Nome
{
    protected override bool ObrigaSegundoNome { get; } = true;

    private NomeContato(string primeiroNome, string segundoNome) : base(primeiroNome, segundoNome)
    {
    }

    public static NomeContato Create(string primeiroNome, string segundoNome)
        => new(primeiroNome, segundoNome);
};