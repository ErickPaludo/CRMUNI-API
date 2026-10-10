namespace CRMUNI.Domain.ObjetosValor.Nomes;

public sealed record NomeEmpresa : Nome
{
    protected override int PrimeiroCaracteresMin { get; } = 10;
    public NomeEmpresa(){}
    public NomeEmpresa(string nome) : base(nome)
    {
    }
}