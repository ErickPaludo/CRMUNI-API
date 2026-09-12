using System.Text.RegularExpressions;
using CRMUNI.DOMAIN.Validacoes.Telefones;
using CRMUNI.DOMAIN.Validacoes.Utilitarios;

namespace CRMUNI.DOMAIN.ObjetosValor.Telefones;

public record Telefone
{
    protected virtual int TamanhoNumeroTelefone { get; } = 12;

    protected Telefone(string numeroTelefone)
    {
        ValidaNulo.Verifica(numeroTelefone, TelefoneMensagens.TelefoneNulo);
        numeroTelefone = Prepara(numeroTelefone);
        Valida(numeroTelefone);
    }

    public static Telefone Create(string numeroTelefone) => new Telefone(numeroTelefone);

    protected virtual void Valida(string numeroTelefone)
    {
        TelefoneValidacao.Verifica(string.IsNullOrWhiteSpace(numeroTelefone),
            TelefoneMensagens.TelefoneObrigatorio);
        TelefoneValidacao.Verifica(numeroTelefone.Length != TamanhoNumeroTelefone,
            TelefoneMensagens.TelefoneCaracteresObrigatorios(TamanhoNumeroTelefone));
    }

    private string Prepara(string numeroTelefone)
    {
        return Regex.Replace(numeroTelefone, @"\D", "");
    }
};