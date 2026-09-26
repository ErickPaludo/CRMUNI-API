namespace CRMUNI.DOMAIN.Validacoes.Entidades.Etapas;

public static class EtapaMensagens
{
    /// <summary>
    /// Codigo de mensagem 6.0.x
    /// Valores de x.x.0 até x.x.10 são reservados para codigos "COMUNS"
    /// </summary>
    
    public static string PropriedadeNula(string propriedade) => $"6.0.0 - {propriedade} não pode ser nulla.";
    public const string TipoInvalido = "6.0.6 - Tipo de setor inválido.";

}