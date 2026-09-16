using CRMUNI.DOMAIN.Validacoes.ObjetosValor.Etapas.Ordens;
using CRMUNI.DOMAIN.Validacoes.Utilitarios;

namespace CRMUNI.DOMAIN.ObjetosValor.Etapas;

public sealed record Ordem
{
    public int Valor { get; }

    private Ordem(int valor)
    {
        ValidaNulo.Verifica(valor, OrdemMensagens.PropriedadeNula("Ordem"));
        Valida(valor);
        Valor = valor;
    }

    public static Ordem Create(int valor)
        => new(valor);

    private void Valida(int valor)
        => OrdemValidacao.Verifica(valor < 0, OrdemMensagens.OrdemMinima);
};