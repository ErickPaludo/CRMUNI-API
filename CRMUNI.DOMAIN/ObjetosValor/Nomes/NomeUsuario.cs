namespace CRMUNI.DOMAIN.ObjetosValor.Nomes;

public sealed record NomeUsuario : Nome
{
    public NomeUsuario(string primeiroNome, string segundoNome) : base(primeiroNome, segundoNome)
    {
    }
};