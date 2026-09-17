using CRMUNI.DOMAIN.Entidades.EntidadesBase;
using CRMUNI.DOMAIN.ObjetosValor.Etapas;
using CRMUNI.DOMAIN.ObjetosValor.Nomes;
using CRMUNI.DOMAIN.Validacoes.Entidades.Etapas;
using CRMUNI.DOMAIN.Validacoes.Utilitarios;

namespace CRMUNI.DOMAIN.Entidades;

public sealed class Etapa : EntidadeIdInt
{
    public Funil Funil { get; }
    public NomeEtapa Nome { get; private set; }
    public Ordem Ordem { get; private set; }
    public List<Atendimento> Atendimentos { get; } = new List<Atendimento>();

    private Etapa(Funil funil,NomeEtapa nome, Ordem ordem)
    {
        ValidaNulo.Verifica(funil,EtapaMensagens.PropriedadeNula("Funil"));
        ValidaNulo.Verifica(nome,EtapaMensagens.PropriedadeNula("Nome"));
        ValidaNulo.Verifica(ordem,EtapaMensagens.PropriedadeNula("Ordem"));
        Funil = funil;
        Nome = nome;
        Ordem = ordem;
    }
    
    public static Etapa  Create(Funil funil,NomeEtapa nome, Ordem ordem)
    =>new (funil,nome, ordem);
}