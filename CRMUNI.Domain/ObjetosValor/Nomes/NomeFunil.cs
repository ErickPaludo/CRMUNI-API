using System;
using System.Collections.Generic;
using System.Text;

namespace CRMUNI.Domaidd.ObjetosValor.Nomes
{
    public sealed record NomeFunil : Nome
    {
        protected override int PrimeiroCaracteresMax { get; } = 25;
        public NomeFunil(){}
        public NomeFunil(string nome) : base(nome)
        {
        }
    }
}
