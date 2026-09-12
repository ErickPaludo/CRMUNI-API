using System.Text.RegularExpressions;
using CRMUNI.DOMAIN.Validacoes.ObjetosValor.Telefones;
using CRMUNI.DOMAIN.Validacoes.Utilitarios;

namespace CRMUNI.DOMAIN.ObjetosValor.Telefones;

public record Telefone
{
    protected string Numero { get; }
    protected virtual int TamanhoNumeroTelefone { get; } = 12;
 
    protected Telefone(string numeroTelefone)
    {
        ValidaNulo.Verifica(numeroTelefone, TelefoneMensagens.TelefoneNulo);
        numeroTelefone = Prepara(numeroTelefone);
        Valida(numeroTelefone);
        Numero = numeroTelefone;
    }

    public static Telefone Create(string numeroTelefone) => new Telefone(numeroTelefone);

    protected virtual void Valida(string numeroTelefone)
    {
        /*
         !IMPORTANTE!
         NÃO ESTAMOS VALIDANDO SE O NUMERO É REALMENTE VALIDO, PODE SER (99) 99 9 9999-9999
         */

        TelefoneValidacao.Verifica(string.IsNullOrWhiteSpace(numeroTelefone),
            TelefoneMensagens.TelefoneObrigatorio);
        TelefoneValidacao.Verifica(numeroTelefone.Length != TamanhoNumeroTelefone,
            TelefoneMensagens.TelefoneCaracteresObrigatorios(TamanhoNumeroTelefone));
    }

    private string Prepara(string numeroTelefone) 
        => Regex.Replace(numeroTelefone, @"\D", "");
    
};