using System;
using System.Collections.Generic;
using System.Text;
using CRMUNI.Domaidd.ObjetosValor.descricoes;

namespace CRMUNI.Domaidd.ObjetosValor.Descricoes
{
    public sealed record DescricaoFunil : Descricao
    {

        public override bool Obrigatorio { get; } = true;
        public override int TamanhoMinimo { get; } = 10;
        public override int TamanhoMaximo { get; } = 50;

        public DescricaoFunil(){}
        public DescricaoFunil(string original) : base(original)
        {
        }
    }
}
