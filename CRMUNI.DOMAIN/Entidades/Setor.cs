using CRMUNI.DOMAIN.Entidades.EntidadesBase;
using CRMUNI.DOMAIN.ObjetosValor.descricoes;
using CRMUNI.DOMAIN.ObjetosValor.Nomes;
using CRMUNI.DOMAIN.Validacoes.Utilitarios;

namespace CRMUNI.DOMAIN.Entidades;

public sealed class Setor : EntidadeIdGuid
{
    public Empresa Empresa { get; }
    public NomeSetor Nome { get; private set; }
    public DescricaoSetor DescricaoSetor { get; private set; }

    private Setor(Empresa empresa, NomeSetor nome, DescricaoSetor descricao)
    {
        ValidaNulo.Verifica(empresa,"Empresa");
        ValidaNulo.Verifica(nome,"Nome");
        ValidaNulo.Verifica(descricao,"Descricao");
        
        Empresa = empresa;
        Nome = nome;
        DescricaoSetor = descricao;
    }

    public static Setor Create(Empresa empresa, NomeSetor nome, DescricaoSetor descricao)
        => new(empresa, nome, descricao);
}