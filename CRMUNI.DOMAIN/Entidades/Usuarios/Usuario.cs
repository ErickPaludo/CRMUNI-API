using CRMUNI.DOMAIN.Entidades.Empresas;
using CRMUNI.DOMAIN.Entidades.EntidadesBase;
using CRMUNI.DOMAIN.ObjetosValor.Funcionarios;
using CRMUNI.DOMAIN.ObjetosValor.Geral;
using CRMUNI.DOMAIN.ObjetosValor.Nomes;
using CRMUNI.DOMAIN.Validacoes.Entidades.Setores;
using CRMUNI.DOMAIN.Validacoes.Entidades.Usuarios;
using CRMUNI.DOMAIN.Validacoes.Utilitarios;

namespace CRMUNI.DOMAIN.Entidades.Usuarios;

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
    }
}