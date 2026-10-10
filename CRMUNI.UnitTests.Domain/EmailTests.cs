using System;
using CRMUNI.Domain.Execoes;
using FluentAssertions;
using NUnit.Framework;
using CRMUNI.Domain.ObjetosValor.Geral;
using CRMUNI.Domain.Validacoes.ObjetosValor.Emails;

namespace CRMUNI.UnitTests.Domain
{
    [TestFixture]
    public class EmailTests
    {
        // Helper to build an email with a specific total length
        private static string BuildEmail(int totalLength)
        {
            const string domain = "@b.com";               // 4 characters (including '@')
            int localLength = totalLength - domain.Length;
            if (localLength <= 0) throw new ArgumentException("Length too small for a valid address.");

            return new string('a', localLength) + domain;
        }

        [Test]
        public void Create_DeveCriarEmail_QuandoEnderecoValido()
        {
            var email = Email.Create("teste@exemplo.com");
            email.Endereco.Should().Be("teste@exemplo.com");
        }

        [Test]
        public void Create_DeveRemoverEspacosDasExtremidades()
        {
            var email = Email.Create("  TESTE@EXEMPLO.COM  ");
            email.Endereco.Should().Be("teste@exemplo.com");
        }

        [Test]
        public void Create_DeveConverterEmailParaMinusculo()
        {
            var email = Email.Create("TeStE@ExEmPlO.CoM");
            email.Endereco.Should().Be("teste@exemplo.com");
        }

        [Test]
        public void Create_DeveLancarExcecao_QuandoEmailNulo()
        {
            Action act = () => Email.Create(null!);
            act.Should()
               .Throw<ExceptionDomain>()
               .WithMessage(EmailMensagens.EmailNullo);
        }

        [Test]
        public void Create_DeveLancarExcecao_QuandoEmailVazio()
        {
            Action act = () => Email.Create(string.Empty);
            act.Should()
               .Throw<EmailValidacao>()
               .WithMessage(EmailMensagens.EmailExigido);
        }

        [Test]
        public void Create_DeveLancarExcecao_QuandoEmailPossuiApenasEspacos()
        {
            Action act = () => Email.Create("    ");
            act.Should()
               .Throw<EmailValidacao>()
               .WithMessage(EmailMensagens.EmailExigido);
        }

        [Test]
        public void Create_DeveLancarExcecao_QuandoEmailPossuiEspaco()
        {
            Action act = () => Email.Create("teste @exemplo.com");
            act.Should()
               .Throw<EmailValidacao>()
               .WithMessage(EmailMensagens.EmailInvalido);
        }

        [TestCase("teste")]
        [TestCase("teste@")]
        [TestCase("@email.com")]
        [TestCase("teste@email")]
        [TestCase("teste@email.com.com")]
        [TestCase("testeemail.com")]
        [TestCase("tes.comte@email.com.com")]
        public void Create_DeveLancarExcecao_QuandoFormatoInvalido(string enderecoInvalido)
        {
            Action act = () => Email.Create(enderecoInvalido);
            act.Should()
               .Throw<EmailValidacao>()
               .WithMessage(EmailMensagens.EmailInvalido);
        }

        [Test]
        public void Create_DeveLancarExcecao_QuandoEmailAbaixoDoTamanhoMinimo()
        {
            // Email com 5 caracteres (menor que MinEndereco = 7) e formato válido
            Action act = () => Email.Create("a@b.cr"); 
            act.Should()
               .Throw<EmailValidacao>()
               .WithMessage(EmailMensagens.EmailMinimo(Email.MinEndereco));
        }

        [Test]
        public void Create_DeveCriarEmail_QuandoEmailNoTamanhoMinimo()
        {
            // 6 caracteres – o menor tamanho aceito
            var email = Email.Create("aa@b.cr");
            email.Endereco.Should().Be("aa@b.cr");
        }

        [Test]
        public void Create_DeveCriarEmail_QuandoEmailNoTamanhoMaximo()
        {
            var email = Email.Create(BuildEmail(Email.MaxEndereco));
            email.Endereco.Length.Should().Be(Email.MaxEndereco);
        }

        [Test]
        public void Create_DeveLancarExcecao_QuandoEmailAcimaDoTamanhoMaximo()
        {
            Action act = () => Email.Create(BuildEmail(Email.MaxEndereco + 1));
            act.Should()
               .Throw<EmailValidacao>()
               .WithMessage(EmailMensagens.EmailMaximo(Email.MaxEndereco));
        }
    }
}
