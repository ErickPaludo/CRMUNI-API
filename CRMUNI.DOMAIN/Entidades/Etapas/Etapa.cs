using CRMUNI.DOMAIN.Entidades.Atendimentos;
using CRMUNI.DOMAIN.Entidades.EntidadesBase;
using CRMUNI.DOMAIN.Entidades.Funis;
using CRMUNI.DOMAIN.ObjetosValor.Etapas;
using CRMUNI.DOMAIN.ObjetosValor.Nomes;
using CRMUNI.DOMAIN.Validacoes.Entidades.Etapas;
using CRMUNI.DOMAIN.Validacoes.Utilitarios;

namespace CRMUNI.DOMAIN.Entidades.Etapas;

public sealed class Etapa : EntidadeIdInt
{
    public EEtapa Tipo { get; }
    public Funil Funil { get; }
    public NomeEtapa Nome { get; private set; }
    public Ordem Ordem { get; private set; }
    public List<Atendimento> Atendimentos { get; } = new List<Atendimento>();
    //TODO criar prazo de atendimento maximo
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