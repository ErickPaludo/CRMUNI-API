using CRMUNI.DOMAIN.ObjetosValor.descricoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRMUNI.DOMAIN.ObjetosValor.Descricoes
{
    public sealed record DescricaoFunil : Descricao
    {

        public override bool Obrigatorio { get; } = true;
        public override int TamanhoMinimo { get; } = 10;
        public override int TamanhoMaximo { get; } = 50;

        private DescricaoFunil(string original) : base(original)
        {
        }

        public static DescricaoFunil Create(string descricao)
            => new(descricao);

    }
}
