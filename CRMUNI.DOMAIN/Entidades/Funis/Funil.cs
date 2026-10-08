using System.ComponentModel.DataAnnotations.Schema;
using CRMUNI.DOMAIN.Entidades.Empresas;
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
        public Empresa Empresa { get; }
        public EFunil Tipo { get;}    
        public NomeFunil Nome { get; private set; }
        public DescricaoFunil Descricao { get; private set; }
        [NotMapped]
        public List<Etapa> Estapas { get; } = new List<Etapa>();

        public Funil()
        {
        }

        public Funil(Empresa empresa,EFunil tipo, NomeFunil nome,DescricaoFunil descricao)
        {
            ValidaNulo.Verifica(empresa, FunilMensagens.PropriedadeNula("Empresa"));
            ValidaNulo.Verifica(tipo, FunilMensagens.PropriedadeNula("Tipo"));
            ValidaNulo.Verifica(nome, FunilMensagens.PropriedadeNula("Nome"));
            ValidaNulo.Verifica(descricao, FunilMensagens.PropriedadeNula("Descricao"));
            
            ValidaEnum<EFunil>.Verifica(tipo,FunilMensagens.TipoInvalido);
            
            Empresa = empresa;
            Tipo = tipo;
            Nome = nome;
            Descricao = descricao;
        }
    }
}
