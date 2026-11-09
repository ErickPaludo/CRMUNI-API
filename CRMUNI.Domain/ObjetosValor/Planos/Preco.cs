using CRMUNI.Domain.Validacoes.ObjetosValor.Planos;
using CRMUNI.Domain.Validacoes.Utilitarios;

namespace CRMUNI.Domain.ObjetosValor.Planos;

public record Preco
{
    // Limite da coluna decimal(18,2) em PlanoConfig
    public const decimal Maximo = 9_999_999_999_999_999.99m;
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
        PrecoValidacao.Verifica(decimal.Round(valor, 2) != valor, PrecoMensagens.CasasDecimaisInvalidas);
        PrecoValidacao.Verifica(valor > Maximo, PrecoMensagens.ValorMaximoExcedido);
    }
}