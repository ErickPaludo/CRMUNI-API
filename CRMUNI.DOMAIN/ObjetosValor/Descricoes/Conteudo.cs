namespace CRMUNI.DOMAIN.ObjetosValor.descricoes;

public sealed record Conteudo : Descricao
{
    public override bool Obrigatorio { get; } = true;
    public override int TamanhoMinimo { get; } = 1;
    public override int TamanhoMaximo { get; } = 1000;

    private Conteudo(string texto) : base(texto)
    {
    }

    public static Conteudo Create(string texto)
        => new(texto);
};