using System.Text.RegularExpressions;
using CRMUNI.Domain.Validacoes.ObjetosValor.Telefones;
using CRMUNI.Domain.Validacoes.Utilitarios;

namespace CRMUNI.Domain.ObjetosValor.Telefones;

public record Telefone
{
    public string Numero { get; }
    public virtual int TamanhoNumeroTelefone { get; } = 12;
 
    public Telefone(){}
    protected Telefone(string numeroTelefone)
    {
        ValidaNulo.Verifica(numeroTelefone, TelefoneMensagens.TelefoneNulo);
        numeroTelefone = Prepara(numeroTelefone);
        Valida(numeroTelefone);
        Numero = numeroTelefone;
    }
    public static Telefone Create(string numeroTelefone) => new Telefone(numeroTelefone);

    private string Prepara(string numeroTelefone) 
        => Regex.Replace(numeroTelefone, @"\D", "");

    private void Valida(string numeroTelefone)
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

    
};