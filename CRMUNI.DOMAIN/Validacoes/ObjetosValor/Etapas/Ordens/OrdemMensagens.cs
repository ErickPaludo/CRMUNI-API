namespace CRMUNI.DOMAIN.Validacoes.ObjetosValor.Etapas.Ordens;

public static class OrdemMensagens
{
    /// <summary>
    /// Codigo de mensagem 0.6.x
    /// Valores de x.x.0 até x.x.10 são reservados para codigos "COMUNS"
    /// </summary>
    public static string PropriedadeNula(string propriedade) => $"6.6.0 - {propriedade} não pode ser nula.";
    public static string OrdemMinima => $"6.6.11 - Ordem deve ser maior que 0.";
}