namespace CRMUNI.DOMAIN.Validacoes.Entidades.Setores;

public class SetorMensagens
{
    /// <summary>
    /// Codigo de mensagem 1.0.x
    /// Valores de x.x.0 até x.x.10 são reservados para codigos "COMUNS"
    /// </summary>
    public static string PropriedadeNula(string propriedade) => $"2.0.0 - {propriedade} não pode ser nulla.";
}