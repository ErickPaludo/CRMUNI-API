namespace CRMUNI.DOMAIN.Validacoes.Entidades.Atendimentos;

public static class AtendimentoMensagens
{
    //Codigo de mensagem 5.0.x
    //Valores de x.x.0 até x.x.10 são reservados para codigos "COMUNS"

    public static string PropriedadeNula(string propriedade) => $"5.0.0 - {propriedade} não pode ser nulla.";
}