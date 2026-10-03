using System;
using CRMUNI.DOMAIN.ObjetosValor.Nomes;
using CRMUNI.DOMAIN.Validacoes.ObjetosValor.Nomes;
using CRMUNI.DOMAIN.Execoes;
using FluentAssertions;
using NUnit.Framework;

namespace CRMUNI.UnitTests.Domain
{
    [TestFixture]
    public class NomeTests
    {
        // Helper to create a valid string of a given length (letters only)
        private static string MakeLetters(int length) => new string('a', length);

        [Test]
        public void Create_DeveCriarNomeUsuario_QuandoNomesValidos()
        {
            var nome = new NomeUsuario("Joao", "Silva");
            nome.Primeiro.Should().Be("Joao");
            nome.Segundo.Should().Be("Silva");
            nome.Completo.Should().Be("Joao Silva");
        }

        [Test]
        public void Create_DeveCriarNomeEtapa_QuandoApenasPrimeiroNomeValido()
        {
            var nome = new NomeEtapa("ProjetoX");
            nome.Primeiro.Should().Be("ProjetoX");
            nome.Segundo.Should().BeNull();
            nome.Completo.Should().Be("ProjetoX");
        }

        [Test]
        public void Create_DeveLancarExcecao_QuandoPrimeiroNomeMuitoCurto()
        {
            Action act = () => new NomeUsuario(MakeLetters(2), "Silva");
            act.Should()
               .Throw<NomeValidacao>()
               .WithMessage(NomeMensagens.PrimeiroNomeCaracteresMinimo(3));
        }

        [Test]
        public void Create_DeveLancarExcecao_QuandoPrimeiroNomeMuitoLongo()
        {
            Action act = () => new NomeUsuario(MakeLetters(51), "Silva");
            act.Should()
               .Throw<NomeValidacao>()
               .WithMessage(NomeMensagens.PrimeiroNomeCaracteresMaximo(50));
        }

        [Test]
        public void Create_DeveLancarExcecao_QuandoSegundoNomeMuitoCurto()
        {
            Action act = () => new NomeUsuario("Joao", MakeLetters(2));
            act.Should()
               .Throw<NomeValidacao>()
               .WithMessage(NomeMensagens.SegundoNomeCaracteresMinimo(3));
        }

        [Test]
        public void Create_DeveLancarExcecao_QuandoSegundoNomeMuitoLongo()
        {
            Action act = () => new NomeUsuario("Joao", MakeLetters(51));
            act.Should()
               .Throw<NomeValidacao>()
               .WithMessage(NomeMensagens.SegundoNomeCaracteresMaximo(50));
        }

        [Test]
        public void Create_DeveLancarExcecao_QuandoPrimeiroNomeContemCaracterInvalido()
        {
            Action act = () => new NomeUsuario("Jo4o", "Silva");
            act.Should()
               .Throw<NomeValidacao>()
               .WithMessage(NomeMensagens.NomeInvalido);
        }

        [Test]
        public void Create_DeveLancarExcecao_QuandoSegundoNomeContemCaracterInvalido()
        {
            Action act = () => new NomeUsuario("Joao", "Silv@" );
            act.Should()
               .Throw<NomeValidacao>()
               .WithMessage(NomeMensagens.NomeInvalido);
        }

        [Test]
        public void Create_DeveLancarExcecao_QuandoPrimeiroNomeNulo()
        {
            Action act = () => new NomeUsuario(null!, "Silva");
            act.Should()
               .Throw<ExceptionDomain>()
               .WithMessage(NomeMensagens.NomeNulo);
        }
        [Test]
        public void Create_DeveLancarExcecao_QuandoSegundoNomeNulo()
        {
            Action act = () => new NomeUsuario("Joao", null!);
            act.Should()
                .Throw<ExceptionDomain>()
                .WithMessage(NomeMensagens.NomeNulo);
        }
    }
}
