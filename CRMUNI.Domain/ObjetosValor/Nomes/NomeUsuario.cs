namespace CRMUNI.Domain.ObjetosValor.Nomes;

public sealed record NomeUsuario : Nome
{
    public NomeUsuario(){}
    public NomeUsuario(string primeiroNome, string segundoNome) : base(primeiroNome, segundoNome)
    {
    }
};