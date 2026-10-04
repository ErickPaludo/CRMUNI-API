using System;
using CRMUNI.DOMAIN.ObjetosValor.Atendimentos;
using CRMUNI.DOMAIN.Validacoes.ObjetosValor.Atendimentos.Agendamentos;
using CRMUNI.DOMAIN.Execoes;
using FluentAssertions;
using NUnit.Framework;

namespace CRMUNI.UnitTests.Domain
{
    [TestFixture]
    public class AgendamentoTests
    {
        private static readonly DateTime BaseDate = new DateTime(2023, 01, 01, 12, 0, 0, DateTimeKind.Utc);

        [Test]
        public void Create_DeveCriarAgendamento_QuandoMinutosValidos()
        {
            // minutos >= MinPadrao (15) deve criar sem exceção
            int minutos = 30;
            Action act = () => _ = Agendamento.Create(BaseDate, minutos);
            act.Should().NotThrow();
            var ag = Agendamento.Create(BaseDate, minutos);
            ag.Data.Should().Be(BaseDate.AddMinutes(minutos));
        }

        [Test]
        public void Create_DeveCriarAgendamento_ComMinutosPadrao_QuandoSemMinutos()
        {
            // uso do método sem minutos adiciona MinPadrao (15)
            var before = DateTime.UtcNow;
            var ag = Agendamento.Create();
            var after = DateTime.UtcNow;
            // O horário deve estar entre before+15 e after+15 (considerando pequenas variações)
            ag.Data.Should().BeAfter(before.AddMinutes(14)).And.BeBefore(after.AddMinutes(16));
        }

        [Test]
        public void Create_DeveLancarAgendamentoValidacao_QuandoMinutosAbaixoDoMinimo()
        {
            // minutos menor que 15 deve lançar AgendamentoValidacao
            int minutos = 10;
            Action act = () => _ = Agendamento.Create(BaseDate, minutos);
            act.Should()
               .Throw<AgendamentoValidacao>()
               .WithMessage(AgendamentoMesagens.TempoMinimo(15));
        }
    }
}
