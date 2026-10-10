namespace CRMUNI.Domain.Validacoes.Entidades.Planos;

public class PlanoMensagens
{
    //Codigo de mensagem 9.0.x
    //Valores de x.x.0 até x.x.10 são reservados para codigos "COMUNS"
    
    public static string PropriedadeNula(string propriedade) => $"9.0.0 - {propriedade} não pode ser nulla.";
    public static string SituacaoInvalida = "9.0.6 - Situação não pode ser nulla.";
}