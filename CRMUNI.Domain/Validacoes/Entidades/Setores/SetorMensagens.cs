namespace CRMUNI.Domain.Validacoes.Entidades.Setores;

public static class SetorMensagens
{
    // Codigo de mensagem 1.0.x
    // Valores de x.x.0 até x.x.10 são reservados para codigos "COMUNS"

    public static string PropriedadeNula(string propriedade) => $"1.0.0 - {propriedade} não pode ser nulla.";
    public const string TipoInvalido = "1.0.6 - Tipo de setor inválido.";
    
    public const string PossuiAtendimentosAtivos = "1.0.11 - Funcionario possuí atendimentos em andamento.";
    public const string EmailsIdenticos = "1.0.12 - Insira um email diferente do atual.";
    public const string SenhasIdenticos = "1.0.14 - A nova senha deve ser diferente da anterior.";
}