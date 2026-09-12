using CRMUNI.DOMAIN.Entidades.EntidadesBase;
using CRMUNI.DOMAIN.ObjetosValor.Documentos;
using CRMUNI.DOMAIN.ObjetosValor.Geral;
using CRMUNI.DOMAIN.ObjetosValor.Nomes;
using CRMUNI.DOMAIN.ObjetosValor.Telefones;
using CRMUNI.DOMAIN.Validacoes.Entidades.Empresas;
using CRMUNI.DOMAIN.Validacoes.Utilitarios;

namespace CRMUNI.DOMAIN.Entidades;

public sealed class Empresa : EntidadeIdGuid
{
    public Email Email { get; private set; }
    public NomeEmpresa Nome { get; set; }
    public Telefone Telefone { get; set; }
    public Cnpj Cnpj { get; set; }
    //public string Senha { get; set; } --Realmente nescessario?

    private Empresa(Email email, NomeEmpresa nome, Telefone telefone, Cnpj cnpj)
    {
        ValidaNulo.Verifica(email,EmpresaMensagens.PropriedadeNula("Email"));
        ValidaNulo.Verifica(nome,EmpresaMensagens.PropriedadeNula("Nome"));
        ValidaNulo.Verifica(telefone,EmpresaMensagens.PropriedadeNula("Telefone"));
        ValidaNulo.Verifica(cnpj,EmpresaMensagens.PropriedadeNula("Cnpj"));
        
        Email = email;
        Nome = nome;
        Telefone = telefone;
        Cnpj = cnpj;
    }

    public static Empresa Create(Email email, NomeEmpresa nomeEmpresa, Telefone telefone, Cnpj cnpj)
        => new(email, nomeEmpresa, telefone, cnpj);
}