using System.ComponentModel.DataAnnotations.Schema;
using CRMUNI.Domaidd.Entidades.Atendimentos;
using CRMUNI.Domaidd.Entidades.EntidadesBase;
using CRMUNI.Domaidd.Entidades.Funis;
using CRMUNI.Domaidd.ObjetosValor.Etapas;
using CRMUNI.Domaidd.ObjetosValor.Nomes;
using CRMUNI.Domaidd.Validacoes.Entidades.Etapas;
using CRMUNI.Domaidd.Validacoes.Utilitarios;

namespace CRMUNI.Domaidd.Entidades.Etapas;

public sealed class Etapa : EntidadeIdInt
{
    public EEtapa Tipo { get; }
    public Funil Funil { get; }
    public NomeEtapa Nome { get; private set; }
    public Ordem Ordem { get; private set; }
    public List<Atendimento> Atendimentos { get; } = new List<Atendimento>();
    //TODO criar prazo de atendimento maximo
    
    public Etapa(){}
    private Etapa(EEtapa tipo ,Funil funil,NomeEtapa nome, Ordem ordem)
    {
        ValidaNulo.Verifica(funil,EtapaMensagens.PropriedadeNula("Funil"));
        ValidaNulo.Verifica(nome,EtapaMensagens.PropriedadeNula("Nome"));
        ValidaNulo.Verifica(ordem,EtapaMensagens.PropriedadeNula("Ordem"));
        ValidaNulo.Verifica(tipo,EtapaMensagens.PropriedadeNula("Tipo"));
        
        ValidaEnum<EEtapa>.Verifica(tipo,EtapaMensagens.TipoInvalido);
        
        Tipo = tipo;
        Funil = funil;
        Nome = nome;
        Ordem = ordem;
    }
    
    public static Etapa  Create(EEtapa tipo,Funil funil,NomeEtapa nome, Ordem ordem)
    =>new (tipo,funil,nome, ordem);
}