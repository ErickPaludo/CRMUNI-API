using CRMUNI.Domain.Validacoes.Entidades.Setores;
using CRMUNI.Domain.Entidades.Empresas;
using CRMUNI.Domain.Entidades.EntidadesBase;
using CRMUNI.Domain.ObjetosValor.Funcionarios;
using CRMUNI.Domain.ObjetosValor.Geral;
using CRMUNI.Domain.ObjetosValor.Nomes;
using CRMUNI.Domain.Validacoes.Entidades.Usuarios;
using CRMUNI.Domain.Validacoes.Utilitarios;

namespace CRMUNI.Domain.Entidades.Usuarios;

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