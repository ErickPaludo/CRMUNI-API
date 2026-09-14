using CRMUNI.DOMAIN.Validacoes.ObjetosValor.Funcionarios.Senhas;
using CRMUNI.DOMAIN.Validacoes.Utilitarios;

namespace CRMUNI.DOMAIN.ObjetosValor.Funcionarios;

public sealed record Senha
{
    public string Salt { get; }
    public string Hash { get; }

    public Senha() { }
    private Senha(string salt, string hash)
    {
        Salt = Preparar(salt);
        Hash = Preparar(hash);
    }

    public static Senha Create(string salt, string hash)
    {
        return new Senha(salt, hash);
    }

    public void AtualizaSenha(Senha senha)
    {
        ValidaNulo.Verifica(senha, MensagensUsuarios.SENHA_NULA);
        SenhaValidacao.Verifica(this == senha, MensagensUsuarios.MESMA_SENHA);
    }
    private static string Preparar(string valor)
    {
        SenhaValidacao.Verifica(string.IsNullOrWhiteSpace(valor), MensagensUsuarios.SENHA_VAZIA);
        return valor.Trim();
    }
};