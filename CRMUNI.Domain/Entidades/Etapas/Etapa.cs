using System.ComponentModel.DataAnnotations.Schema;
using CRMUNI.Domain.Entidades.Atendimentos;
using CRMUNI.Domain.Entidades.EntidadesBase;
using CRMUNI.Domain.Entidades.Funis;
using CRMUNI.Domain.ObjetosValor.Etapas;
using CRMUNI.Domain.ObjetosValor.Nomes;
using CRMUNI.Domain.Validacoes.Entidades.Etapas;
using CRMUNI.Domain.Validacoes.Utilitarios;

namespace CRMUNI.Domain.Entidades.Etapas;

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