using CRMUNI.Domain.Validacoes.ObjetosValor.Funcionarios.Senhas;
using CRMUNI.Domain.Validacoes.Utilitarios;

namespace CRMUNI.Domain.ObjetosValor.Funcionarios;

public sealed record Senha
{
    public string Salt { get; }
    public string Hash { get; }

    public Senha()
    {
    }

    public Senha(string senha,string confirmacaoSenha, string salt, string hash)
    {
        ValidaNulo.Verifica(senha, SenhaMensagens.ValidaNulo("Senha"));
        ValidaNulo.Verifica(confirmacaoSenha, SenhaMensagens.ValidaNulo("Confirmação de senha"));
        ValidaNulo.Verifica(salt, SenhaMensagens.ValidaNulo("Salt"));
        ValidaNulo.Verifica(hash, SenhaMensagens.ValidaNulo("Hash"));
        
        SenhaValidacao.Verifica(!senha.Equals(confirmacaoSenha), SenhaMensagens.SenhasNaoIdenticas);
        
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