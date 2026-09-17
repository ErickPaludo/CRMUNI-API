using CRMUNI.DOMAIN.Entidades.EntidadesBase;
using CRMUNI.DOMAIN.ObjetosValor.descricoes;
using CRMUNI.DOMAIN.ObjetosValor.Nomes;
using CRMUNI.DOMAIN.Validacoes.Entidades.Setores;
using CRMUNI.DOMAIN.Validacoes.Utilitarios;

namespace CRMUNI.DOMAIN.Entidades;

public sealed class Setor : EntidadeIdGuid
{
    public Empresa Empresa { get; }
    public NomeSetor Nome { get; private set; }
    public DescricaoSetor DescricaoSetor { get; private set; }
    public List<Funcionario> Funcionarios { get; private set; } = new List<Funcionario>();

    private Setor(Empresa empresa, NomeSetor nome, DescricaoSetor descricao)
    {
        ValidaNulo.Verifica(empresa,SetorMensagens.PropriedadeNula("Empresa"));
        ValidaNulo.Verifica(nome,SetorMensagens.PropriedadeNula("Nome"));
        ValidaNulo.Verifica(descricao,SetorMensagens.PropriedadeNula("Descricao"));
        
        Empresa = empresa;
        Nome = nome;
        DescricaoSetor = descricao;
    }

    public static Setor Create(Empresa empresa, NomeSetor nome, DescricaoSetor descricao)
        => new(empresa, nome, descricao);
}