namespace CRMUNI.DOMAIN.ObjetosValor.Nomes;

public sealed record NomeFuncionario : Nome
{
    public NomeFuncionario(string primeiroNome, string segundoNome) : base(primeiroNome, segundoNome)
    {
    }
};