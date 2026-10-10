using CRMUNI.Domaidd.Validacoes.ObjetosValor.Planos;
using CRMUNI.Domaidd.Validacoes.Utilitarios;

namespace CRMUNI.Domaidd.ObjetosValor.Planos;

public record Preco
{
    public decimal Valor { get; }
    public Preco() { }
    public Preco(decimal valor)
    {
        ValidaValor(valor);
        Valor = valor;
    }

    public Preco Soma(Preco Preco)
    {
        ValidaNulo.Verifica(Preco, PrecoMensagens.PrecoNulo);
        return new Preco(Valor + Preco.Valor);
    }

    public Preco Subtrai(Preco Preco)
    {
        ValidaNulo.Verifica(Preco, PrecoMensagens.PrecoNulo);
        return new Preco(Valor - Preco.Valor);
    }

    public Preco Porcentagem(Preco Preco)
    {
        ValidaNulo.Verifica(Preco, PrecoMensagens.PrecoNulo);
        return new Preco(Valor + (Preco.Valor / 100));
    }

    private void ValidaValor(decimal valor)
    {
        ValidaNulo.Verifica(valor, PrecoMensagens.PrecoNulo);
        PrecoValidacao.Verifica(valor <= 0, PrecoMensagens.SaldoInvalido);
    }
}