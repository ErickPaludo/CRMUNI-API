namespace CRMUNI.Domain.Validacoes.Entidades.Autenticacoes;

public static class AutenticacaoMensagens
{
    //Codigo de mensagem 10.0.x
    //Valores de x.x.0 até x.x.10 são reservados para codigos "COMUNS"

    public static string PropriedadeNula(string propriedade) => $"10.0.0 - {propriedade} não pode ser nulla.";
    public static string RefreshTokenInvalido = $"10.0.4 - Refresh token invalido.";
    
}