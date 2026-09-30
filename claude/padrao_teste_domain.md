# padrao_teste_domain

## Visão geral

- **Objetivo** – apresentar, de forma padronizada, o padrão adotado para a escrita de testes unitários.
- **Escopo** – aplicável a todas as classes que possuam validações/regras de negócio.

---

## 1. Estrutura de testes

| Aspecto | Como foi feito | Motivo |
|--------|---------------|--------|
| **Framework** | NUnit (atributos `[Test]` e `[TestCase]`) + FluentAssertions para asserções | NUnit já está no projeto; FluentAssertions oferece sintaxe fluente e mensagens claras |
| **Nome dos métodos** | `Metodo_Resultado_Condicao` (ex.: `Create_DeveCriarEmail_QuandoEnderecoValido`) | Deixa evidente o que está sendo testado, o resultado esperado e a condição |
| **Atributos** | - `[Test]` para testes únicos<br>- `[TestCase]` para múltiplos valores da mesma regra | Evita duplicação de código; cada caso gera um teste independente |
| **Métodos auxiliares** | `BuildEmail(int totalLength)` para gerar endereços com tamanho exato; const `domain = "@b.c"` | Garante controle de comprimento mantendo endereço válido (`MailAddress`) |
| **Validação de exceções** | ```csharp\nAction act = () => Email.Create(...);\nact.Should()\n   .Throw<ExceptionDomain>() // ou EmailValidacao\n   .WithMessage(EmailMensagens.<Mensagem>);\n``` | Verifica não só a exceção, mas também a mensagem correspondente ao contrato da classe |
| **Cobertura de regras** | - Criação bem‑sucedida (`Create_DeveCriarEmail_QuandoEnderecoValido`)<br>- Trim e lower‑case (`Create_DeveRemoverEspacosDasExtremidades`, `Create_DeveConverterEmailParaMinusculo`)<br>- Nulidade/vazio/espacos (`..._QuandoEmailNulo`, `..._QuandoEmailVazio`, `..._QuandoEmailPossuiApenasEspacos`)<br>- Espaço interno (`..._QuandoEmailPossuiEspaco`)<br>- Formato inválido (`[TestCase]`)<br>- Tamanho mínimo/máximo usando `Email.MinEndereco` / `Email.MaxEndereco`<br>- Mensagens de exceção específicas | Garante que todos os requisitos da classe são validados de forma explícita |
| **Independência** | Cada teste cria seu próprio e‑mail, sem depender de estado externo | Garante que a execução de um teste não influencia outro (princípio unitário) |
| **Estrutura da classe de teste** | ```csharp\n[TestFixture]\npublic class EmailTests\n{\n    //helpers …\n    [Test] …\n    [TestCase] …\n}\n``` | `[TestFixture]` indica ao NUnit que a classe contém testes; helpers são `private static` |
| **Uso de constantes da classe** | Utiliza `Email.MinEndereco` e `Email.MaxEndereco` ao invés de valores “mágicos” | Mantém os testes sincronizados com a lógica de negócios; mudanças nos limites são refletidas automaticamente |
| **Mensagens de falha** | `.WithMessage(...)` compara a mensagem lançada com a constante em `EmailMensagens` | Garante consistência entre implementação e documentação de mensagens |

---

## 2. Estratégia resumida

1. Preparar valores de entrada (válidos e inválidos).<br>2. Executar `Email.Create`.
3. Asserir o resultado esperado (objeto criado ou exceção).
4. Verificar a mensagem de erro quando aplicável.

---

## 3. Modelo de código (esqueleto)

```csharp
using System;
using FluentAssertions;
using NUnit.Framework;
using SeuProjeto.Namespace;

namespace SeuProjeto.UnitTests
{
    [TestFixture]
    public class <NomeDaClasse>Tests
    {
        // Helpers -------------------------------------------------
        private static <Tipo> BuildSomething(int tamanho) { /* ... */ }

        // Testes de sucesso --------------------------------------
        [Test]
        public void <Metodo>_Deve<Resultado>_Quando<Condição>() { /* ... */ }

        // Testes de falha ----------------------------------------
        [Test]
        public void <Metodo>_DeveLancarExcecao_Quando<Condição>()
        {
            Action act = () => <NomeDaClasse>.<Metodo>(/* params */);
            act.Should()
               .Throw<ExcecaoEsperada>()
               .WithMessage(<MensagemEsperada>);
        }

        // Testes de fronteira ------------------------------------
        [Test]
        public void <Metodo>_DeveLancarExcecao_QuandoTamanhoAbaixoDoMinimo() { /* ... */ }

        // Testes parametrizados ---------------------------------
        [TestCase("inv1")]
        [TestCase("inv2")]
        public void <Metodo>_DeveLancarExcecao_QuandoFormatoInvalido(string entrada)
        {
            Action act = () => <NomeDaClasse>.<Metodo>(entrada);
            act.Should().Throw<ExcecaoEsperada>().WithMessage(<Mensagem>);
        }
    }
}
```

---

## 4. Checklist rápido

- [ ] Framework e biblioteca de asserções declarados
- [ ] Classe marcada com `[TestFixture]`
- [ ] Nome dos métodos no padrão `Metodo_Resultado_Condicao`
- [ ] Uso de `[Test]` e `[TestCase]` adequado
- [ ] Helpers privados e estáticos
- [ ] Exceções verificadas com `.Throw<T>()` e `.WithMessage()`
- [ ] Constantes da classe usadas (sem números mágicos)
- [ ] Testes são independentes
- [ ] Todas as regras de negócio listadas e cobertas
- [ ] Mensagens de falha validadas

---

*Este documento pode ser copiado e adaptado para outras classes de domínio, garantindo consistência nos testes do seu projeto.*
