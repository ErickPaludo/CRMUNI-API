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
            var senha = "senha";
            var salt = "salttxt";
            var hash = "hashtxt";
            Action act = () => _ = new Senha(senha, senha, salt, hash);
            act.Should().NotThrow();
        }

        [Test]
        public void Create_DeveTrimarSaltEHash()
        {
            var senha = "senha";
            var salt = Prepare("salttxt");
            var hash = Prepare("hashtxt");
            var senhaObj = new Senha(senha, senha, salt, hash);
            senhaObj.Salt.Should().Be("salttxt");
            senhaObj.Hash.Should().Be("hashtxt");
        }

        [Test]
        public void Create_DeveLancarExceptionDomain_QuandoSenhaNula()
        {
            Action act = () => _ = new Senha(null!, "senha", "salt", "hash");
            act.Should()
               .Throw<ExceptionDomain>()
               .WithMessage(SenhaMensagens.ValidaNulo("Senha"));
        }

        [Test]
        public void Create_DeveLancarExceptionDomain_QuandoConfirmacaoSenhaNula()
        {
            Action act = () => _ = new Senha("senha", null!, "salt", "hash");
            act.Should()
               .Throw<ExceptionDomain>()
               .WithMessage(SenhaMensagens.ValidaNulo("Confirmação de senha"));
        }

        [Test]
        public void Create_DeveLancarExceptionDomain_QuandoSaltNulo()
        {
            Action act = () => _ = new Senha("senha", "senha", null!, "hash");
            act.Should()
               .Throw<ExceptionDomain>()
               .WithMessage(SenhaMensagens.ValidaNulo("Salt"));
        }

        [Test]
        public void Create_DeveLancarExceptionDomain_QuandoHashNulo()
        {
            Action act = () => _ = new Senha("senha", "senha", "salt", null!);
            act.Should()
               .Throw<ExceptionDomain>()
               .WithMessage(SenhaMensagens.ValidaNulo("Hash"));
        }

        [Test]
        public void Create_DeveLancarSenhaValidacao_QuandoSaltVazio()
        {
            Action act = () => _ = new Senha("senha", "senha", string.Empty, "hash");
            act.Should()
               .Throw<SenhaValidacao>()
               .WithMessage(SenhaMensagens.SenhaObrigatoria);
        }

        [Test]
        public void Create_DeveLancarSenhaValidacao_QuandoHashVazio()
        {
            Action act = () => _ = new Senha("senha", "senha", "salt", string.Empty);
            act.Should()
               .Throw<SenhaValidacao>()
               .WithMessage(SenhaMensagens.SenhaObrigatoria);
        }

        [Test]
        public void Create_DeveLancarSenhaValidacao_QuandoSenhasDiferentes()
        {
            Action act = () => _ = new Senha("senha", "confirmacao", "salt", "hash");
            act.Should()
               .Throw<SenhaValidacao>()
               .WithMessage(SenhaMensagens.SenhasNaoIdenticas);
        }

        [Test]
        public void Create_DeveLancarSenhaValidacao_QuandoSenhasDiferentesPorCaso()
        {
            Action act = () => _ = new Senha("Senha123", "senha123", "salt", "hash");
            act.Should()
               .Throw<SenhaValidacao>()
               .WithMessage(SenhaMensagens.SenhasNaoIdenticas);
        }

        [Test]
        public void AtualizaSenha_DeveLancarExceptionDomain_QuandoSenhaNula()
        {
            var senha = new Senha("senha", "senha", "salt", "hash");
            Action act = () => senha.AtualizaSenha(null!);
            act.Should()
               .Throw<ExceptionDomain>()
               .WithMessage(SenhaMensagens.ValidaNulo("Senha"));
        }

        [Test]
        public void AtualizaSenha_DeveLancarSenhaValidacao_QuandoSenhasIdenticas()
        {
            var senha = new Senha("senha", "senha", "salt", "hash");
            Action act = () => senha.AtualizaSenha(senha);
            act.Should()
               .Throw<SenhaValidacao>()
               .WithMessage(SenhaMensagens.SenhasIdenticas);
        }
    }
}
