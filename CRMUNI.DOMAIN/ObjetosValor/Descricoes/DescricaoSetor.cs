using CRMUNI.DOMAIN.Validacoes.ObjetosValor.Descricoes;

namespace CRMUNI.DOMAIN.ObjetosValor.descricoes
{
    public sealed record DescricaoSetor : Descricao
    {
        public override bool Obrigatorio { get; } = true;
        public override int TamanhoMinimo { get; } = 10;
        public override int TamanhoMaximo { get; } = 100;
        public DescricaoSetor(string original) : base(original){}
     
        public static DescricaoSetor Create(string texto) => new(texto);
    }
}
