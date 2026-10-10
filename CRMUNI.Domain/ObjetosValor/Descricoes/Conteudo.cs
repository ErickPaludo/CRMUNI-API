namespace CRMUNI.Domaidd.ObjetosValor.descricoes;

public sealed record Conteudo : Descricao
{
    public override bool Obrigatorio { get; } = true;
    public override int TamanhoMinimo { get; } = 1;
    public override int TamanhoMaximo { get; } = 1000;

    public Conteudo(){}
    public Conteudo(string texto) : base(texto)
    {
    }
};