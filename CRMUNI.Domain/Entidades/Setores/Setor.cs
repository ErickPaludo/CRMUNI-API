using CRMUNI.Domain.Entidades.Contatos;
using CRMUNI.Domain.Entidades.Empresas;
using CRMUNI.Domain.Entidades.EntidadesBase;
using CRMUNI.Domain.Entidades.Funcionarios;
using CRMUNI.Domain.ObjetosValor.descricoes;
using CRMUNI.Domain.ObjetosValor.Nomes;
using CRMUNI.Domain.Validacoes.Entidades.Setores;
using CRMUNI.Domain.Validacoes.Utilitarios;

namespace CRMUNI.Domain.Entidades.Setores;

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