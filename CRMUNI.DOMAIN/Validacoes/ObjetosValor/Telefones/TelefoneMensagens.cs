namespace CRMUNI.DOMAIN.Validacoes.ObjetosValor.Telefones;

public class TelefoneMensagens
{
    /// <summary>
    /// Codigo de mensagem 0.2.x
    /// Valores de x.x.0 até x.x.10 são reservados para codigos "COMUNS"
    /// </summary>
    
    public const string TelefoneNulo = "0.2.0 - Telefone/Celular não pode ser nullo";

    public const string TelefoneObrigatorio = "0.2.1 - Telefone/Celular deve ser informado.";

    public static string TelefoneCaracteresObrigatorios(int telefoneCaracteresMax) =>
        $"0.2.3 - Telefone/Celular deve possuir no máximo {telefoneCaracteresMax} dígitos.";
    
    public const string TelefoneInvalido = "0.2.4 - Telefone/Celular informado está inválido.";

}