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
        public NomeFunil Nome { get; private set; }
        public DescricaoFunil Descricao { get; private set; }
        public List<Etapa> Estapas { get; } = new List<Etapa>();
        private Funil(NomeFunil nome,DescricaoFunil descricao)
        {
            ValidaNulo.Verifica(nome, FunilMensagens.PropriedadeNula("Nome"));
            ValidaNulo.Verifica(descricao, FunilMensagens.PropriedadeNula("Descricao"));

            Nome = nome;
            Descricao = descricao;
        }

        public static Funil Create(NomeFunil nome, DescricaoFunil descricao)
            => new(nome, descricao);
    }
}
