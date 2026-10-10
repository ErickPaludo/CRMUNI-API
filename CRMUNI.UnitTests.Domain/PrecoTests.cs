using System;
using CRMUNI.DOMAIN.Execoes;
using CRMUNI.DOMAIN.ObjetosValor.Planos;
using CRMUNI.DOMAIN.Validacoes.ObjetosValor.Planos;
using FluentAssertions;
using NUnit.Framework;

namespace CRMUNI.UnitTests.Domain
{
    [TestFixture]
    public class PrecoTests
    {
        [Test]
        public void Construtor_DeveCriarPreco_QuandoValorValido()
        {
            // Arrange
            decimal valorEsperado = 100.50m;

            // Act
            var preco = new Preco(valorEsperado);

            // Assert
            preco.Valor.Should().Be(valorEsperado);
        }

        [Test]
        public void Construtor_DeveLancarExcecao_QuandoValorForZero()
        {
            // Act
            Action act = () => new Preco(0);

            // Assert
            act.Should()
               .Throw<PrecoValidacao>()
               .WithMessage(PrecoMensagens.SaldoInvalido);
        }

        [Test]
        public void Construtor_DeveLancarExcecao_QuandoValorForNegativo()
        {
            // Act
            Action act = () => new Preco(-10.00m);

            // Assert
            act.Should()
               .Throw<PrecoValidacao>()
               .WithMessage(PrecoMensagens.SaldoInvalido);
        }

        [Test]
        public void Soma_DeveRetornarNovoPrecoComSoma()
        {
            // Arrange
            var preco1 = new Preco(100m);
            var preco2 = new Preco(50m);

            // Act
            var resultado = preco1.Soma(preco2);

            // Assert
            resultado.Valor.Should().Be(150m);
        }

        [Test]
        public void Soma_DeveLancarExcecao_QuandoPrecoForNulo()
        {
            // Arrange
            var preco = new Preco(100m);

            // Act
            Action act = () => preco.Soma(null!);

            // Assert
            act.Should()
               .Throw<ExceptionDomain>()
               .WithMessage(PrecoMensagens.PrecoNulo);
        }

        [Test]
        public void Subtrai_DeveRetornarNovoPrecoComSubtracao()
        {
            // Arrange
            var preco1 = new Preco(100m);
            var preco2 = new Preco(40m);

            // Act
            var resultado = preco1.Subtrai(preco2);

            // Assert
            resultado.Valor.Should().Be(60m);
        }

        [Test]
        public void Subtrai_DeveLancarExcecao_SeResultadoForNegativoOuZero()
        {
            // Arrange
            var preco1 = new Preco(100m);
            var preco2 = new Preco(100m);
            var preco3 = new Preco(110m);

            // Act
            Action zero = () => preco1.Subtrai(preco2);
            Action negativo = () => preco1.Subtrai(preco3);

            // Assert
            zero.Should().Throw<PrecoValidacao>().WithMessage(PrecoMensagens.SaldoInvalido);
            negativo.Should().Throw<PrecoValidacao>().WithMessage(PrecoMensagens.SaldoInvalido);
        }

        [Test]
        public void Porcentagem_DeveRetornarValorCalculado()
        {
            // Arrange
            var preco1 = new Preco(100m);
            var preco2 = new Preco(10m); // Representando 10%?

            // Act
            var resultado = preco1.Porcentagem(preco2);

            // Assert
            // De acordo com a implementação: Valor + (Preco.Valor / 100)
            // 100 + (10 / 100) = 100 + 0.1 = 100.1
            resultado.Valor.Should().Be(100.1m);
        }

        [Test]
        public void Porcentagem_DeveLancarExcecao_QuandoPrecoForNulo()
        {
            // Arrange
            var preco = new Preco(100m);

            // Act
            Action act = () => preco.Porcentagem(null!);

            // Assert
            act.Should()
               .Throw<ExceptionDomain>()
               .WithMessage(PrecoMensagens.PrecoNulo);
        }
    }
}
