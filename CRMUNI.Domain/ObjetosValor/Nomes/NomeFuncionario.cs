namespace CRMUNI.Domaidd.ObjetosValor.Nomes;

public sealed record NomeFuncionario : Nome
{
    public NomeFuncionario(){}
    public NomeFuncionario(string primeiroNome, string segundoNome) : base(primeiroNome, segundoNome)
    {
    }
};