using System;
using CRMUNI.Domain.Entidades.Empresas;
using CRMUNI.Domain.Entidades.Planos;
using CRMUNI.Domain.Execoes;
using CRMUNI.Domain.ObjetosValor.Descricoes;
using CRMUNI.Domain.ObjetosValor.Nomes;
using CRMUNI.Domain.ObjetosValor.Planos;
using CRMUNI.Domain.Validacoes.Entidades.Planos;
using FluentAssertions;
using NUnit.Framework;

namespace CRMUNI.UnitTests.Domain
{
    [TestFixture]
    public class PlanoTests
    {
        private static DescricaoPlano BuildDescricao()
            => new DescricaoPlano(new string('a', 100));

        [Test]
        public void Construtor_DeveAtribuirPreco_QuandoDadosValidos()
        {
            var plano = new Plano(new Empresa(), new NomePlano("Basico"), new Preco(99.90m), BuildDescricao());

            plano.Preco.Valor.Should().Be(99.90m);
        }

        [Test]
        public void Construtor_DeveLancarExcecao_QuandoPrecoNulo()
        {
            Action act = () => new Plano(new Empresa(), new NomePlano("Basico"), null!, BuildDescricao());

            act.Should()
               .Throw<ExceptionDomain>()
               .WithMessage(PlanoMensagens.PropriedadeNula("Preco"));
        }
    }
}
