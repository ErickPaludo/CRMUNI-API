using CRMUNI.Domaidd.Entidades.Empresas;
using CRMUNI.Domaidd.Entidades.EntidadesBase;
using CRMUNI.Domaidd.ObjetosValor.Funcionarios;
using CRMUNI.Domaidd.ObjetosValor.Geral;
using CRMUNI.Domaidd.ObjetosValor.Nomes;
using CRMUNI.Domaidd.Validacoes.Entidades.Usuarios;
using CRMUNI.Domaidd.Validacoes.Utilitarios;
using CRMUNI.Domaidd.Validacoes.Entidades.Setores;

namespace CRMUNI.Domaidd.Entidades.Usuarios;

public sealed class Usuario : EntidadeIdGuid
{
    public Empresa Empresa { get; }
    public NomeUsuario Nome { get; private set; }
    public Email Email { get; private set; }
    public Senha Senha { get; private set; }

    public Usuario(){}
    public Usuario(Empresa empresa, NomeUsuario nome, Email email,Senha senha)
    {
        ValidaNulo.Verifica(empresa,UsuarioMensagens.PropriedadeNula("Empresa"));
        ValidaNulo.Verifica(nome,UsuarioMensagens.PropriedadeNula("Nome"));
        ValidaNulo.Verifica(senha,UsuarioMensagens.PropriedadeNula("Senha"));
        ValidaNulo.Verifica(email,UsuarioMensagens.PropriedadeNula("Email"));
        
        Empresa = empresa;
        Nome = nome;
        Senha = senha;
        Email = email;
    }
}