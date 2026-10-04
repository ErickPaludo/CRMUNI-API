using System;
using CRMUNI.DOMAIN.ObjetosValor.Etapas;
using CRMUNI.DOMAIN.Validacoes.ObjetosValor.Etapas.Ordens;
using CRMUNI.DOMAIN.Execoes;
using FluentAssertions;
using NUnit.Framework;

namespace CRMUNI.UnitTests.Domain
{
    [TestFixture]
    public class OrdemTests
    {
        [Test]
        public void Create_DeveCriarOrdem_QuandoValorValido()
        {
            // Valor positivo
            var ordem = Ordem.Create(5);
            ordem.Should().NotBeNull();
            ordem.Valor.Should().Be(5);
        }

        [Test]
        public void Create_DeveLancarOrdemValidacao_QuandoValorNegativo()
        {
            Action act = () => Ordem.Create(-1);
            act.Should()
               .Throw<OrdemValidacao>()
               .WithMessage(OrdemMensagens.OrdemMinima);
        }
    }
}
