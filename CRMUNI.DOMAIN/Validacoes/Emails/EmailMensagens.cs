using CRMUNI.DOMAIN.Validacoes.Geral;

namespace CRMUNI.DOMAIN.Validacoes.Emails;

public static class EmailMensagens
{
    /// <summary>
    /// Codigo de mensagem 0.1.x
    /// Valores de x.x.0 até x.x.10 são reservados para codigos "COMUNS"
    /// </summary>
    public const string EmailNullo = "0.0.0 - Email nullo.";

    public const string EmailExigido = "0.0.1 - O email deve ser informado.";

    public static string EmailMinimo(int minEndereco) =>
        $"0.0.2 - Email deve conter no mínimo {minEndereco} caracteres.";

    public static string EmailMaximo(int maxEndereco) =>
        $"0.0.3 - Email deve conter no maximo {maxEndereco} caracteres.";

    public const string EmailInvalido = "0.0.4 - O email informado está em um formato inválido.";
}