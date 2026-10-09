using CRMUNI.DOMAIN.Entidades.Empresas;
using CRMUNI.DOMAIN.Entidades.EntidadesBase;
using CRMUNI.DOMAIN.ObjetosValor.Descricoes;
using CRMUNI.DOMAIN.ObjetosValor.Nomes;
using CRMUNI.DOMAIN.Validacoes.Entidades.Planos;
using CRMUNI.DOMAIN.Validacoes.Utilitarios;

namespace CRMUNI.DOMAIN.Entidades.Planos;

public sealed class Plano : EntidadeIdGuid
{
    public Empresa Empresa { get; }
    public NomePlano Nome { get; private set; }
    public DescricaoPlano Descricao { get; private set; }
    public EPlanoSituacao Situacao { get; private set; }

    public Plano(Empresa empresa, NomePlano nome, DescricaoPlano descricao, EPlanoSituacao situacao)
    {
        ValidaNulo.Verifica(empresa, PlanoMensagens.PropriedadeNula("Empresa"));
        ValidaNulo.Verifica(nome, PlanoMensagens.PropriedadeNula("Nome"));
        ValidaNulo.Verifica(descricao, PlanoMensagens.PropriedadeNula("Descrição"));
        
        ValidaEnum<EPlanoSituacao>.Verifica(situacao,PlanoMensagens.SituacaoInvalida);
        
        Empresa = empresa;
        Nome = nome;
        Descricao = descricao;
        Situacao = situacao;
    }
}