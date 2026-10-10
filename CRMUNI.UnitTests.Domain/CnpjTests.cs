using CRMUNI.Domain.ObjetosValor.Documentos;
using CRMUNI.Domain.Validacoes.ObjetosValor.Documentos;
using CRMUNI.Domain.Execoes;
using FluentAssertions;

namespace CRMUNI.UnitTests.Domain
{
    [TestFixture]
    public class CnpjTests
    {
        // Helper para gerar uma string de dígitos com o tamanho desejado
        private static string MakeDigits(int length) => new string('1', length);

        [Test]
        public void Create_DeveCriarCnpj_QuandoNumeroValido()
        {
            var numero = MakeDigits(14);
            Action act = () => _ = new Cnpj(numero);
            act.Should().NotThrow();
        }

        [Test]
        public void Create_DeveCriarCnpj_RemovendoCaracteresNaoNumericos()
        {
            var raw = "12.345.678/0001-90"; // após remover não numeros: 12345678000190 (14)
            Action act = () => _ = new Cnpj(raw);
            act.Should().NotThrow();
        }

        [Test]
        public void Create_DeveLancarExceptionDomain_QuandoNumeroNulo()
        {
            Action act = () => _ = new Cnpj(null!);
            act.Should()
                .Throw<ExceptionDomain>()
                .WithMessage(DocumentoMensagens.DocumentonNulo);
        }

        [Test]
        public void Create_DeveLancarDocumentoValidacao_QuandoNumeroVazio()
        {
            Action act = () => _ = new Cnpj(string.Empty);
            act.Should()
                .Throw<DocumentoValidacao>()
                .WithMessage(DocumentoMensagens.DocumentoObrigatorio);
        }

        [Test]
        public void Create_DeveLancarDocumentoValidacao_QuandoNumeroComTamanhoIncorreto()
        {
            var curtos = MakeDigits(12); // tamanho errado (deve ser 14)
            Action act = () => _ = new Cnpj(curtos);
            act.Should()
                .Throw<DocumentoValidacao>()
                .WithMessage(DocumentoMensagens.DocumentoCaracteresObrigatorios(14));
        }

        [Test]
        public void Create_DeveTerCodigoLimpo_AposCriacao()
        {
            var raw = "12.345.678/0001-90"; // será convertido para 12345678000190
            var cnpj = new Cnpj(raw);
            // acesso ao campo protegido "Codigo" via reflexão
            var field = cnpj.Codigo; 
            field.Should().Be("12345678000190");
        }
    }
}