using CRMUNI.DOMAIN.Entidades.EntidadesBase;
using CRMUNI.DOMAIN.Entidades.Etapas;
using CRMUNI.DOMAIN.ObjetosValor.Descricoes;
using CRMUNI.DOMAIN.ObjetosValor.Nomes;
using CRMUNI.DOMAIN.Validacoes.Entidades.Funis;
using CRMUNI.DOMAIN.Validacoes.Utilitarios;

namespace CRMUNI.DOMAIN.Entidades.Funis
{
    public sealed class Funil : EntidadeIdInt
    {
        public EFunil Tipo { get;}    
        public NomeFunil Nome { get; private set; }
        public DescricaoFunil Descricao { get; private set; }
        public List<Etapa> Estapas { get; } = new List<Etapa>();
        private Funil(EFunil tipo, NomeFunil nome,DescricaoFunil descricao)
        {
            ValidaNulo.Verifica(tipo, FunilMensagens.PropriedadeNula("Tipo"));
            ValidaNulo.Verifica(nome, FunilMensagens.PropriedadeNula("Nome"));
            ValidaNulo.Verifica(descricao, FunilMensagens.PropriedadeNula("Descricao"));
            
            ValidaEnum<EFunil>.Verifica(tipo,FunilMensagens.TipoInvalido);
            
            Tipo = tipo;
            Nome = nome;
            Descricao = descricao;
        }

        public static Funil Create(EFunil tipo, NomeFunil nome, DescricaoFunil descricao)
            => new(tipo,nome, descricao);
    }
}
