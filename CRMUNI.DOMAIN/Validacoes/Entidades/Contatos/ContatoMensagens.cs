namespace CRMUNI.DOMAIN.Validacoes.Entidades.Contatos;

public static class ContatoMensagens
{
    //Codigo de mensagem 3.0.x
    //Valores de x.x.0 até x.x.10 são reservados para codigos "COMUNS"

    public static string PropriedadeNula(string propriedade) => $"3.0.0 - {propriedade} não pode ser nulla.";

    public static string EnumInvalido(string nome) => $"3.0.6 - {nome} inválida.";
}
