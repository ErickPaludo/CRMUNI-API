using CRMUNI.DOMAIN.Validacoes.ObjetosValor.Nomes;
using CRMUNI.DOMAIN.Validacoes.Utilitarios;

namespace CRMUNI.DOMAIN.ObjetosValor.Nomes;

public abstract record Nome
{
    public string Primeiro { get; }
    public string? Segundo { get; }

    public string Completo => $"{Primeiro} {Segundo}";

    protected virtual bool ObrigaSegundoNome { get; } = true;

    protected virtual int PrimeiroCaracteresMin { get; } = 3;
    protected virtual int PrimeiroCaracteresMax { get; } = 50;
    protected virtual int SegundoCaracteresMin { get; } = 3;
    protected virtual int SegundoCaracteresMax { get; } = 50;

    protected Nome(string primeiroNome, string segundoNome)
    {
        ValidaObrigatoriedadeSegundoNome();
        VerificaPrimeiro(primeiroNome);
        VerificaSegundo(segundoNome);
        primeiroNome = Prepara(primeiroNome);
        segundoNome = Prepara(segundoNome);
        Primeiro = Prepara(primeiroNome);
        Segundo = Prepara(segundoNome);
    }

    protected Nome(string primeiroNome)
    {
        ValidaObrigatoriedadeSegundoNome();

        VerificaPrimeiro(primeiroNome);
        primeiroNome = Prepara(primeiroNome);
        Primeiro = Prepara(primeiroNome);
    }

    private static string Prepara(string valor)
    {
        NomeValicao.Verifica(string.IsNullOrWhiteSpace(valor), NomeMensagens.NomeObrigatorio);
        valor = valor.Trim();
        return valor;
    }

    private void ValidaObrigatoriedadeSegundoNome()
        => NomeValicao.Verifica(!ObrigaSegundoNome, NomeMensagens.SegundoNomeObrigatorio);

    private void VerificaPrimeiro(string valor)
    {
        ValidaNulo.Verifica(valor, NomeMensagens.NomeNulo);
        NomeValicao.Verifica(!valor.All(c => char.IsLetter(c) || c == ' '), NomeMensagens.NomeInvalido);
        NomeValicao.Verifica(valor.Length > PrimeiroCaracteresMax,
            NomeMensagens.PrimeiroNomeCaracteresMaximo(PrimeiroCaracteresMax));
        NomeValicao.Verifica(valor.Length < PrimeiroCaracteresMin,
            NomeMensagens.PrimeiroNomeCaracteresMinimo(PrimeiroCaracteresMin));
    }

    private void VerificaSegundo(string valor)
    {
        NomeValicao.Verifica(!valor.All(c => char.IsLetter(c) || c == ' '), NomeMensagens.NomeInvalido);
        NomeValicao.Verifica(valor.Length > SegundoCaracteresMin,
            NomeMensagens.SegundoNomeCaracteresMaximo(PrimeiroCaracteresMax));
        NomeValicao.Verifica(valor.Length < SegundoCaracteresMax,
            NomeMensagens.SegundoNomeCaracteresMinimo(PrimeiroCaracteresMax));
    }
}