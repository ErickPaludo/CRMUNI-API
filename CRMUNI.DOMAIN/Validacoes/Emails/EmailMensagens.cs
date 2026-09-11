using CRMUNI.DOMAIN.Validacoes.Geral;

namespace CRMUNI.DOMAIN.Validacoes.Emails;

public static class EmailMensagens
{
    public const string EmailInvalido = "O email informado está em um formato inválido.";
    public static string EmailMinimo(int minEndereco) => $"Email deve conter no mínimo {minEndereco} caracteres.";
    public static string EmailMaximo(int maxEndereco)=> $"Email deve conter no maximo {maxEndereco} caracteres.";
}