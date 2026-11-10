using System;
using CRMUNI.Domain.Entidades.Atendimentos;
using CRMUNI.Domain.Entidades.Contatos;
using CRMUNI.Domain.Entidades.Etapas;
using CRMUNI.Domain.Entidades.Funcionarios;
using CRMUNI.Domain.Validacoes.Entidades.Atendimentos;
using FluentAssertions;
using NUnit.Framework;

namespace CRMUNI.UnitTests.Domain
{
    [TestFixture]
    public class AtendimentoTests
    {
        // Helpers -------------------------------------------------
        private static Atendimento BuildAtendimento()
            => Atendimento.Create(new Contato(), new Funcionario(), new Etapa());

        private static Atendimento BuildFinalizado()
        {
            Atendimento atendimento = BuildAtendimento();
            atendimento.Finalizar();
            return atendimento;
        }

        // Testes de sucesso --------------------------------------
        [Test]
        public void Finalizar_DeveDefinirSituacaoFinalizado_QuandoAtendimentoAberto()
        {
            Atendimento atendimento = BuildAtendimento();

            atendimento.Finalizar();

            atendimento.Situacao.Should().Be(EAtendimentoSituacao.Finalizado);
        }

        [Test]
        public void AlterarEtapa_DeveTrocarEtapa_QuandoAtendimentoAberto()
        {
            Atendimento atendimento = BuildAtendimento();
            var etapa = new Etapa();

            atendimento.AlterarEtapa(etapa);

            atendimento.Etapa.Should().BeSameAs(etapa);
        }

        [Test]
        public void TransferirFuncionario_DeveTrocarFuncionario_QuandoAtendimentoAberto()
        {
            Atendimento atendimento = BuildAtendimento();
            var destino = new Funcionario();

            atendimento.TransferirFuncionario(destino);

            atendimento.Funcionario.Should().BeSameAs(destino);
        }

        // Testes de falha ----------------------------------------
        [Test]
        public void Finalizar_DeveLancarExcecao_QuandoAtendimentoJaFinalizado()
        {
            Action act = () => BuildFinalizado().Finalizar();

            act.Should().Throw<AtendimentoValidacao>().WithMessage(AtendimentoMensagens.AtendimentoFinalizado);
        }

        [Test]
        public void AlterarEtapa_DeveLancarExcecao_QuandoAtendimentoFinalizado()
        {
            Action act = () => BuildFinalizado().AlterarEtapa(new Etapa());

            act.Should().Throw<AtendimentoValidacao>().WithMessage(AtendimentoMensagens.AtendimentoFinalizado);
        }

        [Test]
        public void TransferirFuncionario_DeveLancarExcecao_QuandoAtendimentoFinalizado()
        {
            Action act = () => BuildFinalizado().TransferirFuncionario(new Funcionario());

            act.Should().Throw<AtendimentoValidacao>().WithMessage(AtendimentoMensagens.AtendimentoFinalizado);
        }

        [Test]
        public void AlterarEtapa_DeveLancarExcecao_QuandoEtapaNula()
        {
            Action act = () => BuildAtendimento().AlterarEtapa(null!);

            act.Should().Throw<Exception>().WithMessage(AtendimentoMensagens.PropriedadeNula("Etapa"));
        }

        [Test]
        public void TransferirFuncionario_DeveLancarExcecao_QuandoFuncionarioNulo()
        {
            Action act = () => BuildAtendimento().TransferirFuncionario(null!);

            act.Should().Throw<Exception>().WithMessage(AtendimentoMensagens.PropriedadeNula("Funcionario"));
        }
    }
}
