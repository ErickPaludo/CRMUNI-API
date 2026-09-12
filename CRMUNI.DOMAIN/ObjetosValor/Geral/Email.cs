using System.Net.Mail;
using CRMUNI.DOMAIN.Validacoes.ObjetosValor.Emails;
using CRMUNI.DOMAIN.Validacoes.Utilitarios;

namespace CRMUNI.DOMAIN.ObjetosValor.Geral;

public sealed record Email
{
    public string Endereco { get; }
    public static readonly int MaxEndereco = 256;
    public static readonly int MinEndereco = 6;

    private Email(string endereco)
    {
        endereco = Prepara(endereco);
        ValidaFomatacao(endereco);
        ValidaTamanhoCaracteres(endereco);
        Endereco = endereco;
    }

    public static Email Create(string endereco)
    {
        return new Email(endereco);
    }

    private static string Prepara(string email)
    {
        ValidaNulo.Verifica(email, EmailMensagens.EmailNullo);
        EmailValidacao.Verifica(string.IsNullOrWhiteSpace(email), EmailMensagens.EmailExigido);
        email = email.Trim();
        email = email.ToLower();
        return email;
    }

    private static void ValidaFomatacao(string email)
    {
        EmailValidacao.Verifica(email.Contains(" "), EmailMensagens.EmailInvalido);
        EmailValidacao.Verifica(!ValidaFormato(email), EmailMensagens.EmailInvalido);
    }

    private void ValidaTamanhoCaracteres(string email)
    {
        EmailValidacao.Verifica(email.Length < MinEndereco, EmailMensagens.EmailMinimo(MinEndereco));
        EmailValidacao.Verifica(email.Length > MaxEndereco, EmailMensagens.EmailMaximo(MaxEndereco));
    }

    private static bool ValidaFormato(string email)
    {
        try
        {
            _ = new MailAddress(email);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}