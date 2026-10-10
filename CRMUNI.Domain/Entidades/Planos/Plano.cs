using CRMUNI.Domaidd.Entidades.Empresas;
using CRMUNI.Domaidd.Entidades.EntidadesBase;
using CRMUNI.Domaidd.ObjetosValor.Descricoes;
using CRMUNI.Domaidd.ObjetosValor.Nomes;
using CRMUNI.Domaidd.Validacoes.Entidades.Planos;
using CRMUNI.Domaidd.Validacoes.Utilitarios;

namespace CRMUNI.Domaidd.Entidades.Planos;

public sealed class Plano : EntidadeIdGuid
{
    public Empresa Empresa { get; }
    public NomePlano Nome { get; private set; }
    public DescricaoPlano Descricao { get; private set; }
    public EPlanoSituacao Situacao { get; private set; }

    public Plano()
    {
    }

    public Plano(Empresa empresa, NomePlano nome, DescricaoPlano descricao, EPlanoSituacao situacao)
    {
        ValidaNulo.Verifica(empresa, PlanoMensagens.PropriedadeNula("Empresa"));
        ValidaNulo.Verifica(nome, PlanoMensagens.PropriedadeNula("Nome"));
        ValidaNulo.Verifica(descricao, PlanoMensagens.PropriedadeNula("Descrição"));

        ValidaEnum<EPlanoSituacao>.Verifica(situacao, PlanoMensagens.SituacaoInvalida);

        Empresa = empresa;
        Nome = nome;
        Descricao = descricao;
        Situacao = situacao;
    }
}