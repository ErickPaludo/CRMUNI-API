namespace CRMUNI.Domain.ObjetosValor.Nomes;

public sealed record NomeContato : Nome
{
    public NomeContato(){}
    public NomeContato(string primeiroNome, string segundoNome) : base(primeiroNome, segundoNome)
    {
    }
};