using System;
using CRMUNI.Domain.ObjetosValor.descricoes;
using CRMUNI.Domain.Validacoes.ObjetosValor.Descricoes;
using CRMUNI.Domain.Execoes;
using FluentAssertions;
using NUnit.Framework;

namespace CRMUNI.UnitTests.Domain
{
    [TestFixture]
    public class ConteudoTests
    {
        [Test]
        public void Create_DeveCriarConteudo_QuandoTextoValido()
        {
            var conteudo = new Conteudo("Teste");
            conteudo.Texto.Should().Be("Teste");
        }

        [Test]
        public void Create_DeveRemoverEspacosDasExtremidades()
        {
            var conteudo = new Conteudo("  teste  ");
            conteudo.Texto.Should().Be("teste");
        }

        [Test]
        public void Create_DeveLancarExcecao_QuandoTextoNulo()
        {
            Action act = () => new Conteudo(null!);
            act.Should()
               .Throw<ExceptionDomain>()
               .WithMessage(DescricaoMensagens.DescricaoNula);
        }

        [Test]
        public void Create_DeveLancarExcecao_QuandoTextoVazio()
        {
            Action act = () => new Conteudo(string.Empty);
            act.Should()
               .Throw<DescricaoValidacao>()
               .WithMessage(DescricaoMensagens.DescricaoObrigatoria);
        }

        [Test]
        public void Create_DeveLancarExcecao_QuandoTextoSomenteEspacos()
        {
            Action act = () => new Conteudo("   ");
            act.Should()
               .Throw<DescricaoValidacao>()
               .WithMessage(DescricaoMensagens.DescricaoObrigatoria);
        }

        [Test]
        public void Create_DeveLancarExcecao_QuandoTextoAcimaDoTamanhoMaximo()
        {
            var textoLongo = new string('a', 1001);
            Action act = () => new Conteudo(textoLongo);
            act.Should()
               .Throw<DescricaoValidacao>()
               .WithMessage(DescricaoMensagens.DescricaoMaximo(1000));
        }
    }
}
