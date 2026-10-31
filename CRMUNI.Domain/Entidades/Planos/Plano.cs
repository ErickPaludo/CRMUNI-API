using CRMUNI.Domain.Entidades.Empresas;
using CRMUNI.Domain.Entidades.EntidadesBase;
using CRMUNI.Domain.ObjetosValor.Descricoes;
using CRMUNI.Domain.ObjetosValor.Nomes;
using CRMUNI.Domain.Validacoes.Entidades.Planos;
using CRMUNI.Domain.Validacoes.Utilitarios;

namespace CRMUNI.Domain.Entidades.Planos;

public sealed class Plano : EntidadeIdGuid
{
    public Empresa Empresa { get; }
    public NomePlano Nome { get; private set; }
    public DescricaoPlano Descricao { get; private set; }
    public EPlanoSituacao Situacao { get; private set; }

    public Plano()
    {
    }

    public Plano(Empresa empresa, NomePlano nome, DescricaoPlano descricao)
    {
        ValidaNulo.Verifica(empresa, PlanoMensagens.PropriedadeNula("Empresa"));
        ValidaNulo.Verifica(nome, PlanoMensagens.PropriedadeNula("Nome"));
        ValidaNulo.Verifica(descricao, PlanoMensagens.PropriedadeNula("Descrição"));

        Empresa = empresa;
        Nome = nome;
        Descricao = descricao;
        Situacao = EPlanoSituacao.Ativo;
        
        empresa.AddPlanos(this);
    }
}