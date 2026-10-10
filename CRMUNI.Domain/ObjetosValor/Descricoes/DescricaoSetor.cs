using CRMUNI.Domain.Validacoes.ObjetosValor.Descricoes;

namespace CRMUNI.Domain.ObjetosValor.descricoes
{
    public sealed record DescricaoSetor : Descricao
    {
        public override bool Obrigatorio { get; } = true;
        public override int TamanhoMinimo { get; } = 10;
        public override int TamanhoMaximo { get; } = 100;

        public DescricaoSetor(){}
        public DescricaoSetor(string original) : base(original)
        {
        }
    }
}