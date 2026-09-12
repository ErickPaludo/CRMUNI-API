namespace CRMUNI.DOMAIN.Validacoes.ObjetosValor.Nomes;

public static class NomeMensagens
{
    /// <summary>
    /// Codigo de mensagem 0.1.x
    /// Valores de x.x.0 até x.x.10 são reservados para codigos "COMUNS"
    /// </summary>
    public const string NomeNulo = "0.1.0 - Nome não pode ser nullo";

    public const string NomeObrigatorio = "0.1.1 - Nome deve ser informado.";

    public static string PrimeiroNomeCaracteresMaximo(int primeiroCaracteresMax) =>
        $"0.1.3 - Primeiro nome deve possuir no máximo {primeiroCaracteresMax} caracteres.";

    public static string PrimeiroNomeCaracteresMinimo(int primeiroCaracteresMin) =>
        $"0.1.4 - Primeiro nome deve possuir no mínimo {primeiroCaracteresMin} caracteres.";

    public const string NomeInvalido = "0.1.4 - Nome inválido.";
    public const string SegundoNomeObrigatorio = "0.1.5 - Segundo nome deve ser informado.";

    public static string SegundoNomeCaracteresMaximo(int primeiroCaracteresMax) =>
        $"0.1.11 - Primeiro nome deve possuir no máximo {primeiroCaracteresMax} caracteres.";

    public static string SegundoNomeCaracteresMinimo(int primeiroCaracteresMin) =>
        $"0.1.12 - Primeiro nome deve possuir no mínimo {primeiroCaracteresMin} caracteres.";
}