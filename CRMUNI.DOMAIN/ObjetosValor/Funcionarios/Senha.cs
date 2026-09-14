using CRMUNI.DOMAIN.Validacoes.ObjetosValor.Funcionarios.Senhas;
using CRMUNI.DOMAIN.Validacoes.Utilitarios;

namespace CRMUNI.DOMAIN.ObjetosValor.Funcionarios;

public sealed record Senha
{
    public string Salt { get; }
    public string Hash { get; }

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
        ValidaNulo.Verifica(senha, SenhaMensagens.SenhaNula);
        SenhaValidacao.Verifica(this == senha, SenhaMensagens.SenhasIdenticas);
    }
    private static string Preparar(string valor)
    {
        SenhaValidacao.Verifica(string.IsNullOrWhiteSpace(valor), SenhaMensagens.SenhaObrigatoria);
        return valor.Trim();
    }
};