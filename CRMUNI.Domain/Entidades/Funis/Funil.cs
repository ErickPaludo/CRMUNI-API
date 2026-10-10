using System.ComponentModel.DataAnnotations.Schema;
using CRMUNI.Domaidd.Entidades.Empresas;
using CRMUNI.Domaidd.Entidades.EntidadesBase;
using CRMUNI.Domaidd.Entidades.Etapas;
using CRMUNI.Domaidd.ObjetosValor.Descricoes;
using CRMUNI.Domaidd.ObjetosValor.Nomes;
using CRMUNI.Domaidd.Validacoes.Entidades.Funis;
using CRMUNI.Domaidd.Validacoes.Utilitarios;

namespace CRMUNI.Domaidd.Entidades.Funis
{
    public sealed class Funil : EntidadeIdInt
    {
        public Empresa Empresa { get; }
        public EFunil Tipo { get;}    
        public NomeFunil Nome { get; private set; }
        public DescricaoFunil Descricao { get; private set; }
        public List<Etapa> Etapas { get; } = new List<Etapa>();

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
