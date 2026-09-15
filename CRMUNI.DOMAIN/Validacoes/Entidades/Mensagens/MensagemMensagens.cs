namespace CRMUNI.DOMAIN.Validacoes.Entidades.Mensagens;

public static class MensagemMensagens
{
    /// <summary>
    /// Codigo de mensagem 4.0.x
    /// Valores de x.x.0 até x.x.10 são reservados para codigos "COMUNS"
    /// </summary>
    /// 
    public static string PropriedadeNula(string propriedade) => $"4.0.0 - {propriedade} não pode ser nulla.";
}