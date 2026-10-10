using System.ComponentModel.DataAnnotations.Schema;
using CRMUNI.Domain.Entidades.Contatos;
using CRMUNI.Domain.Entidades.EntidadesBase;
using CRMUNI.Domain.Entidades.Funis;
using CRMUNI.Domain.Entidades.Planos;
using CRMUNI.Domain.Entidades.Setores;
using CRMUNI.Domain.ObjetosValor.Documentos;
using CRMUNI.Domain.ObjetosValor.Geral;
using CRMUNI.Domain.ObjetosValor.Nomes;
using CRMUNI.Domain.ObjetosValor.Telefones;
using CRMUNI.Domain.Validacoes.Entidades.Empresas;
using CRMUNI.Domain.Validacoes.Utilitarios;

namespace CRMUNI.Domain.Entidades.Empresas;

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

    public Empresa(Email email, NomeEmpresa nome, Telefone telefone, Cnpj cnpj)
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

    public void AddSetor(Setor setor)
    {
        ValidaNulo.Verifica(setor, EmpresaMensagens.PropriedadeNula("Setor"));
        Setores.Add(setor);
    }  
    public void AddFunil(Funil funil)
    {
        ValidaNulo.Verifica(funil, EmpresaMensagens.PropriedadeNula("Funil"));
        Funils.Add(funil);
    }
}