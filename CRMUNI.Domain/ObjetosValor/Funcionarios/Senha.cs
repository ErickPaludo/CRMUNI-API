using CRMUNI.Domaidd.Validacoes.ObjetosValor.Funcionarios.Senhas;
using CRMUNI.Domaidd.Validacoes.Utilitarios;

namespace CRMUNI.Domaidd.ObjetosValor.Funcionarios;

public sealed record Senha
{
    public string Salt { get; }
    public string Hash { get; }

    public Senha()
    {
    }

    public Senha(string salt, string hash)
    {
        ValidaNulo.Verifica(salt, SenhaMensagens.ValidaNulo("Salt"));
        ValidaNulo.Verifica(hash, SenhaMensagens.ValidaNulo("Hash"));

        Salt = Preparar(salt);
        Hash = Preparar(hash);
    }

    public void AtualizaSenha(Senha senha)
    {
        ValidaNulo.Verifica(senha, SenhaMensagens.ValidaNulo("Senha"));
        SenhaValidacao.Verifica(this == senha, SenhaMensagens.SenhasIdenticas);
    }
    private static string Preparar(string valor)
    {
        SenhaValidacao.Verifica(string.IsNullOrWhiteSpace(valor), SenhaMensagens.SenhaObrigatoria);
        return valor.Trim();
    }
};