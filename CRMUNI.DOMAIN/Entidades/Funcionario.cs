using CRMUNI.DOMAIN.Entidades.EntidadesBase;
using CRMUNI.DOMAIN.ObjetosValor.Funcionarios;
using CRMUNI.DOMAIN.ObjetosValor.Nomes;
using CRMUNI.DOMAIN.Validacoes.Utilitarios;

namespace CRMUNI.DOMAIN.Entidades;

public sealed class Funcionario : EntidadeIdInt
{
    public Setor Setor { get; private set; }
    public NomeFuncionario Nome { get; private set; }
    public Senha Senha { get; private set; }

    private Funcionario(Setor setor, NomeFuncionario nome, Senha senha)
    {
        ValidaNulo.Verifica(setor,"Setor");
        ValidaNulo.Verifica(nome,"Nome");
        ValidaNulo.Verifica(senha,"Setor");

        Setor = setor;
        Nome = nome;
        Senha = senha;
    }

    public static Funcionario Create(Setor setor, NomeFuncionario nome, Senha senha)
        => new(setor, nome, senha);
}