using CRMUNI.DOMAIN.Validacoes.ObjetosValor.Nomes;
using CRMUNI.DOMAIN.Validacoes.Utilitarios;

namespace CRMUNI.DOMAIN.ObjetosValor.Nomes;

public abstract record Nome
{
    public string Primeiro { get; }
    public string? Segundo { get; }

    public string Completo => Segundo is null
        ? Primeiro
        : $"{Primeiro} {Segundo}";
    
    protected virtual int PrimeiroCaracteresMin { get; } = 3;
    protected virtual int PrimeiroCaracteresMax { get; } = 50;
    protected virtual int SegundoCaracteresMin { get; } = 3;
    protected virtual int SegundoCaracteresMax { get; } = 50;

    public Nome(){}
    protected Nome(string primeiroNome, string segundoNome)
    {
        ValidaNulo.Verifica(primeiroNome, NomeMensagens.NomeNulo);
        primeiroNome = Prepara(primeiroNome);
        VerificaPrimeiro(primeiroNome);
        Primeiro = primeiroNome;

        ValidaNulo.Verifica(segundoNome, NomeMensagens.NomeNulo);
        segundoNome = Prepara(segundoNome);
        VerificaSegundo(segundoNome);
        Segundo = segundoNome;
    }

    protected Nome(string primeiroNome)
    {        
        ValidaNulo.Verifica(primeiroNome, NomeMensagens.NomeNulo);
        primeiroNome = Prepara(primeiroNome);
        VerificaPrimeiro(primeiroNome);
        Primeiro = primeiroNome;
    }

    private static string Prepara(string valor)
    {
        NomeValidacao.Verifica(string.IsNullOrWhiteSpace(valor), NomeMensagens.NomeObrigatorio);
        valor = valor.Trim();
        return valor;
    }

    private void VerificaPrimeiro(string valor)
    {
        NomeValidacao.Verifica(!valor.All(c => char.IsLetter(c) || c == ' '), NomeMensagens.NomeInvalido);
        NomeValidacao.Verifica(valor.Length > PrimeiroCaracteresMax,
            NomeMensagens.PrimeiroNomeCaracteresMaximo(PrimeiroCaracteresMax));
        NomeValidacao.Verifica(valor.Length < PrimeiroCaracteresMin,
            NomeMensagens.PrimeiroNomeCaracteresMinimo(PrimeiroCaracteresMin));
    }

    private void VerificaSegundo(string valor)
    {
        NomeValidacao.Verifica(!valor.All(c => char.IsLetter(c) || c == ' '), NomeMensagens.NomeInvalido);
        NomeValidacao.Verifica(valor.Length > SegundoCaracteresMax,
            NomeMensagens.SegundoNomeCaracteresMaximo(SegundoCaracteresMax));
        NomeValidacao.Verifica(valor.Length < SegundoCaracteresMin,
            NomeMensagens.SegundoNomeCaracteresMinimo(SegundoCaracteresMin));
    }
}