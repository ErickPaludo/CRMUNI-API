namespace CRMUNI.DOMAIN.Validacoes.ObjetosValor.Funcionarios.Senhas;

public static class SenhaMensagens
{
    /// <summary>
    /// Codigo de mensagem 0.5.x
    /// Valores de x.x.0 até x.x.10 são reservados para codigos "COMUNS"
    /// </summary>
    public static string ValidaNulo(string valor) => $"0.5.0 - {valor} não pode ser nula";

    public const string SenhaObrigatoria = "0.5.1 - Senha deve ser informada.";
    public const string SenhasIdenticas = "0.5.11 - As senhas são identicas.";
}