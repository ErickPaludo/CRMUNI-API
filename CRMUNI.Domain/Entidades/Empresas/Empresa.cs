using System.ComponentModel.DataAnnotations.Schema;
using CRMUNI.Domaidd.Entidades.Contatos;
using CRMUNI.Domaidd.Entidades.EntidadesBase;
using CRMUNI.Domaidd.Entidades.Funis;
using CRMUNI.Domaidd.Entidades.Planos;
using CRMUNI.Domaidd.Entidades.Setores;
using CRMUNI.Domaidd.ObjetosValor.Documentos;
using CRMUNI.Domaidd.ObjetosValor.Geral;
using CRMUNI.Domaidd.ObjetosValor.Nomes;
using CRMUNI.Domaidd.ObjetosValor.Telefones;
using CRMUNI.Domaidd.Validacoes.Entidades.Empresas;
using CRMUNI.Domaidd.Validacoes.Utilitarios;

namespace CRMUNI.Domaidd.Entidades.Empresas;

public sealed class Empresa : EntidadeIdGuid
{
    public Email Email { get; private set; }
    public NomeEmpresa Nome { get; private set; }
    public Telefone Telefone { get; private set; }
    public Cnpj Cnpj { get; }

    public List<Setor> Setores { get; } = new();
    public List<Funil> Funils { get; } = new();
    public List<Contato> Contatos { get; } = new();
    public List<Plano> Planos { get; } = new();

    public Empresa()
    {
    }

    private Empresa(Email email, NomeEmpresa nome, Telefone telefone, Cnpj cnpj)
    {
        ValidaNulo.Verifica(email, EmpresaMensagens.PropriedadeNula("Email"));
        ValidaNulo.Verifica(nome, EmpresaMensagens.PropriedadeNula("Nome"));
        ValidaNulo.Verifica(telefone, EmpresaMensagens.PropriedadeNula("Telefone"));
        ValidaNulo.Verifica(cnpj, EmpresaMensagens.PropriedadeNula("Cnpj"));

        Email = email;
        Nome = nome;
        Telefone = telefone;
        Cnpj = cnpj;
    }

    public static Empresa Create(Email email, NomeEmpresa nomeEmpresa, Telefone telefone, Cnpj cnpj)
        => new(email, nomeEmpresa, telefone, cnpj);
}