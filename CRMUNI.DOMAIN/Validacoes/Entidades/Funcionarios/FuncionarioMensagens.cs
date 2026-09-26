namespace CRMUNI.DOMAIN.Validacoes.Entidades.Funcionarios;

public static class FuncionarioMensagens
{
    //Codigo de mensagem 2.0.x
    //Valores de x.x.0 até x.x.10 são reservados para codigos "COMUNS"

    public static string PropriedadeNula(string propriedade) => $"2.0.0 - {propriedade} não pode ser nulla.";
}