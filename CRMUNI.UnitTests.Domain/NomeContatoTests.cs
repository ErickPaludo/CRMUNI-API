using System;
using CRMUNI.Domain.ObjetosValor.Nomes;
using CRMUNI.Domain.Validacoes.ObjetosValor.Nomes;
using FluentAssertions;
using NUnit.Framework;

namespace CRMUNI.UnitTests.Domain
{
    [TestFixture]
    public class NomeContatoTests
    {
        [Test]
        public void Create_DeveSepararPrimeiroESegundoNome_QuandoNomeCompleto()
        {
            var nome = NomeContato.Create("  Maria da Silva ");
            nome.Primeiro.Should().Be("Maria");
            nome.Segundo.Should().Be("da Silva");
        }

        [Test]
        public void Create_DeveLancarNomeValidacao_QuandoSemSobrenome()
        {
            Action act = () => NomeContato.Create("Maria");
            act.Should()
               .Throw<NomeValidacao>()
               .WithMessage(NomeMensagens.SegundoNomeObrigatorio);
        }

        [Test]
        public void Create_DeveLancarNomeValidacao_QuandoVazio()
        {
            Action act = () => NomeContato.Create("   ");
            act.Should()
               .Throw<NomeValidacao>()
               .WithMessage(NomeMensagens.NomeObrigatorio);
        }
    }
}
