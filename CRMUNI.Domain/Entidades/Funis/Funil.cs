using System.ComponentModel.DataAnnotations.Schema;
using CRMUNI.Domain.Entidades.Empresas;
using CRMUNI.Domain.Entidades.EntidadesBase;
using CRMUNI.Domain.Entidades.Etapas;
using CRMUNI.Domain.ObjetosValor.Descricoes;
using CRMUNI.Domain.ObjetosValor.Nomes;
using CRMUNI.Domain.Validacoes.Entidades.Funis;
using CRMUNI.Domain.Validacoes.Utilitarios;

namespace CRMUNI.Domain.Entidades.Funis
{
    public sealed class Funil : EntidadeIdInt
    {
        public Empresa Empresa { get; }
        public EFunil Tipo { get;}    
        public NomeFunil Nome { get; private set; }
        public DescricaoFunil Descricao { get; private set; }
        public List<Etapa> Etapas { get; } = new();

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
            
            empresa.AddFunil(this);
        }

        public void AddEtapa(Etapa etapa)
        {
            Etapas.Add(etapa);
        }
    }
}
