using CRMUNI.DOMAIN.Entidades.EntidadesBase;
using CRMUNI.DOMAIN.Entidades.Enumeradores;
using CRMUNI.DOMAIN.ObjetosValor.descricoes;
using CRMUNI.DOMAIN.ObjetosValor.Nomes;
using CRMUNI.DOMAIN.Validacoes.Entidades.Setores;
using CRMUNI.DOMAIN.Validacoes.Utilitarios;

namespace CRMUNI.DOMAIN.Entidades;

public sealed class Setor : EntidadeIdGuid
{
    public ESetor Tipo { get;}
    public Empresa Empresa { get; }
    public NomeSetor Nome { get; private set; }
    public DescricaoSetor DescricaoSetor { get; private set; }
    public List<Funcionario> Funcionarios { get; } = new List<Funcionario>();

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