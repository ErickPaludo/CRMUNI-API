using System;
using CRMUNI.Domain.ObjetosValor.Telefones;
using CRMUNI.Domain.Validacoes.ObjetosValor.Telefones;
using FluentAssertions;
using NUnit.Framework;

namespace CRMUNI.UnitTests.Domain
{
    [TestFixture]
    public class CelularTests
    {
        [Test]
        public void Create_DeveCriarCelular_QuandoNumeroValido()
        {
            var celular = Celular.Create("55 (11) 91234-5678");
            celular.Numero.Should().Be("5511912345678");
        }

        [Test]
        public void Create_DeveLancarTelefoneValidacao_QuandoNumeroSemDdi()
        {
            Action act = () => Celular.Create("(11) 91234-5678");
            act.Should()
               .Throw<TelefoneValidacao>()
               .WithMessage(TelefoneMensagens.TelefoneCaracteresObrigatorios(13));
        }
    }
}
