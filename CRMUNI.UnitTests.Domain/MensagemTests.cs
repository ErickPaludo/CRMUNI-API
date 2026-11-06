using System;
using CRMUNI.Domain.Entidades.Atendimentos;
using CRMUNI.Domain.Entidades.Contatos;
using CRMUNI.Domain.Entidades.Etapas;
using CRMUNI.Domain.Entidades.Funcionarios;
using CRMUNI.Domain.Entidades.Mensagens;
using CRMUNI.Domain.Execoes;
using CRMUNI.Domain.ObjetosValor.descricoes;
using CRMUNI.Domain.Validacoes.Entidades.Mensagens;
using FluentAssertions;
using NUnit.Framework;

namespace CRMUNI.UnitTests.Domain
{
    [TestFixture]
    public class MensagemTests
    {
        // Helpers -------------------------------------------------
        private static Atendimento BuildAtendimento()
            => Atendimento.Create(new Contato(), new Funcionario(), new Etapa());

        // Testes de sucesso --------------------------------------
        [Test]
        public void DoCliente_DeveRegistrarContatoComoRemetente_QuandoAtendimentoValido()
        {
            Atendimento atendimento = BuildAtendimento();

            Mensagem mensagem = Mensagem.DoCliente(atendimento, new Conteudo("Olá"));

            mensagem.Contato.Should().BeSameAs(atendimento.Contato);
            mensagem.Funcionario.Should().BeNull();
            mensagem.EnviadaPeloFuncionario.Should().BeFalse();
            mensagem.Atendimento.Should().BeSameAs(atendimento);
            mensagem.Conteudo.Texto.Should().Be("Olá");
        }

        [Test]
        public void DoFuncionario_DeveRegistrarFuncionarioComoRemetente_QuandoAtendimentoValido()
        {
            Atendimento atendimento = BuildAtendimento();

            Mensagem mensagem = Mensagem.DoFuncionario(atendimento, new Conteudo("Olá"));

            mensagem.Funcionario.Should().BeSameAs(atendimento.Funcionario);
            mensagem.Contato.Should().BeNull();
            mensagem.EnviadaPeloFuncionario.Should().BeTrue();
        }

        // Testes de falha ----------------------------------------
        [Test]
        public void DoCliente_DeveLancarExcecao_QuandoAtendimentoNulo()
        {
            Action act = () => Mensagem.DoCliente(null!, new Conteudo("Olá"));
            act.Should()
               .Throw<ExceptionDomain>()
               .WithMessage(MensagemMensagens.PropriedadeNula("Atendimento"));
        }

        [Test]
        public void DoFuncionario_DeveLancarExcecao_QuandoConteudoNulo()
        {
            Action act = () => Mensagem.DoFuncionario(BuildAtendimento(), null!);
            act.Should()
               .Throw<ExceptionDomain>()
               .WithMessage(MensagemMensagens.PropriedadeNula("Conteudo"));
        }

        [Test]
        public void DoCliente_DeveLancarExcecao_QuandoAtendimentoSemContato()
        {
            Action act = () => Mensagem.DoCliente(new Atendimento(), new Conteudo("Olá"));
            act.Should()
               .Throw<ExceptionDomain>()
               .WithMessage(MensagemMensagens.PropriedadeNula("Contato"));
        }
    }
}
