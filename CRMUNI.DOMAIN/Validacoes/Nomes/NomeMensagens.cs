namespace CRMUNI.DOMAIN.Validacoes.Nomes;

public static class NomeMensagens
{
    public const string NomeNulo = "Nome não pode ser nullo";
    public const string NomeObrigatorio = "Nome deve ser informado.";
    public const string NomeInvalido = "Nome inválido.";
    public const string SegundoNomeObrigatorio = "Segundo nome deve ser informado.";

    public static string PrimeiroNomeCaracteresMaximo(int primeiroCaracteresMax) =>
        $"Primeiro nome deve possuir no máximo {primeiroCaracteresMax} caracteres.";

    public static string PrimeiroNomeCaracteresMinimo(int primeiroCaracteresMin) =>
        $"Primeiro nome deve possuir no mínimo {primeiroCaracteresMin} caracteres.";

    public static string SegundoNomeCaracteresMaximo(int primeiroCaracteresMax) =>
        $"Primeiro nome deve possuir no máximo {primeiroCaracteresMax} caracteres.";

    public static string SegundoNomeCaracteresMinimo(int primeiroCaracteresMin) =>
        $"Primeiro nome deve possuir no mínimo {primeiroCaracteresMin} caracteres.";
}