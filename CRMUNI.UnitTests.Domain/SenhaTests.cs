using System;
using CRMUNI.Domain.ObjetosValor.Funcionarios;
using CRMUNI.Domain.Validacoes.ObjetosValor.Funcionarios.Senhas;
using CRMUNI.Domain.Execoes;
using FluentAssertions;
using NUnit.Framework;

namespace CRMUNI.UnitTests.Domain
{
    [TestFixture]
    public class SenhaTests
    {
        // Helper to create a string with optional surrounding spaces
        private static string Prepare(string value) => $"  {value}  ";

        [Test]
        public void Create_DeveCriarSenha_QuandoValoresValidos()
        {
            var salt = "salttxt";
            var hash = "hashtxt";
            Action act = () => _ = new Senha(salt, hash);
            act.Should().NotThrow();
        }

        [Test]
        public void Create_DeveTrimarSaltEHash()
        {
            var salt = Prepare("salttxt");
            var hash = Prepare("hashtxt");
            var senha = new Senha(salt, hash);
            senha.Salt.Should().Be("salttxt");
            senha.Hash.Should().Be("hashtxt");
        }

        [Test]
        public void Create_DeveLancarExceptionDomain_QuandoSaltNulo()
        {
            Action act = () => _ = new Senha(null!, "hash");
            act.Should()
               .Throw<ExceptionDomain>()
               .WithMessage(SenhaMensagens.ValidaNulo("Salt"));
        }

        [Test]
        public void Create_DeveLancarExceptionDomain_QuandoHashNulo()
        {
            Action act = () => _ = new Senha("salt", null!);
            act.Should()
               .Throw<ExceptionDomain>()
               .WithMessage(SenhaMensagens.ValidaNulo("Hash"));
        }

        [Test]
        public void Create_DeveLancarSenhaValidacao_QuandoSaltVazio()
        {
            Action act = () => _ = new Senha(string.Empty, "hash");
            act.Should()
               .Throw<SenhaValidacao>()
               .WithMessage(SenhaMensagens.SenhaObrigatoria);
        }

        [Test]
        public void Create_DeveLancarSenhaValidacao_QuandoHashVazio()
        {
            Action act = () => _ = new Senha("salt", string.Empty);
            act.Should()
               .Throw<SenhaValidacao>()
               .WithMessage(SenhaMensagens.SenhaObrigatoria);
        }

        [Test]
        public void AtualizaSenha_DeveLancarExceptionDomain_QuandoSenhaNula()
        {
            var senha = new Senha("salt", "hash");
            Action act = () => senha.AtualizaSenha(null!);
            act.Should()
               .Throw<ExceptionDomain>()
               .WithMessage(SenhaMensagens.ValidaNulo("Senha"));
        }

        [Test]
        public void AtualizaSenha_DeveLancarSenhaValidacao_QuandoSenhasIdenticas()
        {
            var senha = new Senha("salt", "hash");
            Action act = () => senha.AtualizaSenha(senha);
            act.Should()
               .Throw<SenhaValidacao>()
               .WithMessage(SenhaMensagens.SenhasIdenticas);
        }
    }
}
