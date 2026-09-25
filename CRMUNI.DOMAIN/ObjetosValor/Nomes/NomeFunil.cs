using System;
using System.Collections.Generic;
using System.Text;

namespace CRMUNI.DOMAIN.ObjetosValor.Nomes
{
    public sealed record NomeFunil : Nome
    {
        protected override bool ObrigaSegundoNome { get; } = false;
        protected override int PrimeiroCaracteresMax { get; } = 25;
        private NomeFunil(string nome) : base(nome)
        {
        }
        public static NomeFunil Create(string primeiroNome)
          => new(primeiroNome);

    }
}
