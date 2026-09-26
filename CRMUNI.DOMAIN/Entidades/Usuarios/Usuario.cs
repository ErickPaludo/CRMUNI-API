using CRMUNI.DOMAIN.Entidades.Empresas;
using CRMUNI.DOMAIN.Entidades.EntidadesBase;
using CRMUNI.DOMAIN.ObjetosValor.Funcionarios;
using CRMUNI.DOMAIN.ObjetosValor.Nomes;

namespace CRMUNI.DOMAIN.Entidades.Usuarios;

public sealed class Usuario : EntidadeIdGuid
{
    public Empresa Empresa { get; }
    public NomeUsuario Nome { get; private set; }
    public Senha Senha { get; private set; }

    private Usuario(Empresa empresa, NomeUsuario nome, Senha senha)
    {
        Empresa = empresa;
        Nome = nome;
        Senha = senha;
    }

    public static Usuario Create(Empresa empresa, NomeUsuario nome, Senha senha)
        => new(empresa, nome, senha);
}