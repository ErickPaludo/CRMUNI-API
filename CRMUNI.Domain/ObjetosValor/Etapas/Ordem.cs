using CRMUNI.Domaidd.Validacoes.ObjetosValor.Etapas.Ordens;
using CRMUNI.Domaidd.Validacoes.Utilitarios;

namespace CRMUNI.Domaidd.ObjetosValor.Etapas;

public sealed record Ordem
{
    public int Valor { get; }

    public Ordem(){}
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