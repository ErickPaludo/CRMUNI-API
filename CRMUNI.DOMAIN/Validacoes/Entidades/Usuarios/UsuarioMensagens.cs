namespace CRMUNI.DOMAIN.Validacoes.Entidades.Usuarios;

public static class UsuarioMensagens
{
    //Codigo de mensagem 8.0.x
    //Valores de x.x .0 até x.x .10 são reservados para codigos "COMUNS"
    
    public static string PropriedadeNula(string propriedade) => $"8.0.0 - {propriedade} não pode ser nulla.";

}