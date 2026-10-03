using System;
using CRMUNI.DOMAIN.ObjetosValor.Telefones;
using CRMUNI.DOMAIN.Validacoes.ObjetosValor.Telefones;
using CRMUNI.DOMAIN.Execoes;
using FluentAssertions;
using NUnit.Framework;

namespace CRMUNI.UnitTests.Domain
{
    [TestFixture]
    public class TelefoneTests
    {
        // Helper to generate a string of digits of a given length
        private static string MakeDigits(int length) => new string('1', length);

        [Test]
        public void Create_DeveCriarTelefone_QuandoNumeroValido()
        {
            // 12 digits as required
            var numero = MakeDigits(12);
            Action act = () => Telefone.Create(numero);
            act.Should().NotThrow();
        }

        [Test]
        public void Create_DeveCriarTelefone_RemovendoCaracteresNaoNumericos()
        {
            // Input with formatting characters; after stripping should be 12 digits
            var raw = "55 (11) 2345-6789"; // digits => 551123456789 (12)
            Action act = () => Telefone.Create(raw);
            act.Should().NotThrow();
        }

        [Test]
        public void Create_DeveLancarExceptionDomain_QuandoNumeroNulo()
        {
            Action act = () => Telefone.Create(null!);
            act.Should()
               .Throw<ExceptionDomain>()
               .WithMessage(TelefoneMensagens.TelefoneNulo);
        }

        [Test]
        public void Create_DeveLancarTelefoneValidacao_QuandoNumeroVazio()
        {
            Action act = () => Telefone.Create(string.Empty);
            act.Should()
               .Throw<TelefoneValidacao>()
               .WithMessage(TelefoneMensagens.TelefoneObrigatorio);
        }

        [Test]
        public void Create_DeveLancarTelefoneValidacao_QuandoNumeroComTamanhoIncorreto()
        {
            // 6 digits, not the required 12
            var curtos = MakeDigits(6);
            Action act = () => Telefone.Create(curtos);
            act.Should()
               .Throw<TelefoneValidacao>()
               .WithMessage(TelefoneMensagens.TelefoneCaracteresObrigatorios(12));
        }
    }
}
