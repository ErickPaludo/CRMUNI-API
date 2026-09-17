using System;
using System.Collections.Generic;
using System.Text;

namespace CRMUNI.DOMAIN.ObjetosValor.Nomes
{
    public sealed record NomeFunil : Nome
    {
        public override bool ObrigaSegundoNome { get; } = false;
        public override int PrimeiroCaracteresMax { get; } = 25;
        private NomeFunil(string nome) : base(nome)
        {
        }
        public static NomeFunil Create(string primeiroNome)
          => new(primeiroNome);

    }
}
