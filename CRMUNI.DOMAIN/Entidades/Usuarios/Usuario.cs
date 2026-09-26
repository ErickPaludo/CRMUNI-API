using CRMUNI.DOMAIN.Entidades.Empresas;
using CRMUNI.DOMAIN.Entidades.EntidadesBase;
using CRMUNI.DOMAIN.ObjetosValor.Funcionarios;
using CRMUNI.DOMAIN.ObjetosValor.Nomes;
using CRMUNI.DOMAIN.Validacoes.Entidades.Setores;
using CRMUNI.DOMAIN.Validacoes.Entidades.Usuarios;
using CRMUNI.DOMAIN.Validacoes.Utilitarios;

namespace CRMUNI.DOMAIN.Entidades.Usuarios;

public sealed class Usuario : EntidadeIdGuid
{
    public Empresa Empresa { get; }
    public NomeUsuario Nome { get; private set; }
    public Senha Senha { get; private set; }

    private Usuario(Empresa empresa, NomeUsuario nome, Senha senha)
    {
        ValidaNulo.Verifica(empresa,UsuarioMensagens.PropriedadeNula("Empresa"));
        ValidaNulo.Verifica(nome,UsuarioMensagens.PropriedadeNula("Nome"));
        ValidaNulo.Verifica(senha,UsuarioMensagens.PropriedadeNula("Senha"));
        Empresa = empresa;
        Nome = nome;
        Senha = senha;
    }

    public static Usuario Create(Empresa empresa, NomeUsuario nome, Senha senha)
        => new(empresa, nome, senha);
}