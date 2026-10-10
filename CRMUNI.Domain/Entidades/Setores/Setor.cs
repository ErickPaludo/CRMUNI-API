using CRMUNI.Domaidd.Entidades.Empresas;
using CRMUNI.Domaidd.Entidades.EntidadesBase;
using CRMUNI.Domaidd.Entidades.Funcionarios;
using CRMUNI.Domaidd.ObjetosValor.descricoes;
using CRMUNI.Domaidd.ObjetosValor.Nomes;
using CRMUNI.Domaidd.Validacoes.Entidades.Setores;
using CRMUNI.Domaidd.Validacoes.Utilitarios;
using CRMUNI.Domaidd.Entidades.Contatos;

namespace CRMUNI.Domaidd.Entidades.Setores;

public sealed class Setor : EntidadeIdGuid
{
    public ESetor Tipo { get;}
    public Empresa Empresa { get; }
    public NomeSetor Nome { get; private set; }
    public DescricaoSetor DescricaoSetor { get; private set; }
    public List<Funcionario> Funcionarios { get; } = new List<Funcionario>();

    public Setor(){}
    private Setor(ESetor tipo, Empresa empresa, NomeSetor nome, DescricaoSetor descricao)
    {
        ValidaNulo.Verifica(empresa,SetorMensagens.PropriedadeNula("Empresa"));
        ValidaNulo.Verifica(nome,SetorMensagens.PropriedadeNula("Nome"));
        ValidaNulo.Verifica(descricao,SetorMensagens.PropriedadeNula("Descricao"));
        ValidaNulo.Verifica(tipo,SetorMensagens.PropriedadeNula("Tipo"));
        
        ValidaEnum<ESetor>.Verifica(tipo, SetorMensagens.TipoInvalido);

        Tipo = tipo;
        Empresa = empresa;
        Nome = nome;
        DescricaoSetor = descricao;
    }

    public static Setor Create(ESetor tipo, Empresa empresa, NomeSetor nome, DescricaoSetor descricao)
        => new(tipo,empresa, nome, descricao);
}