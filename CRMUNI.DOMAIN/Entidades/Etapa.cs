using CRMUNI.DOMAIN.Entidades.EntidadesBase;
using CRMUNI.DOMAIN.ObjetosValor.Etapas;
using CRMUNI.DOMAIN.ObjetosValor.Nomes;
using CRMUNI.DOMAIN.Validacoes.Entidades.Etapas;
using CRMUNI.DOMAIN.Validacoes.Utilitarios;

namespace CRMUNI.DOMAIN.Entidades;

public sealed class Etapa : EntidadeIdInt
{
    //funil
    public NomeEtapa Nome { get; private set; }
    public Ordem Ordem { get; private set; }

    private Etapa(NomeEtapa nome, Ordem ordem)
    {
        ValidaNulo.Verifica(nome,EtapaMensagens.PropriedadeNula("Nome"));
        ValidaNulo.Verifica(ordem,EtapaMensagens.PropriedadeNula("Ordem"));
        Nome = nome;
        Ordem = ordem;
    }
    
    public static Etapa  Create(NomeEtapa nome, Ordem ordem)
    =>new (nome, ordem);
}