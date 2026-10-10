using CRMUNI.Domaidd.ObjetosValor.descricoes;

namespace CRMUNI.Domaidd.ObjetosValor.Descricoes;

public record DescricaoPlano : Descricao
{
    public override int TamanhoMaximo { get; } = 400;
    public override int TamanhoMinimo { get; } = 100;

    public DescricaoPlano()
    {
    }

    public DescricaoPlano(string texto) : base(texto)
    {
    }
};