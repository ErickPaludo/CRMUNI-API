namespace CRMUNI.DOMAIN.Validacoes.ObjetosValor.Atendimentos.Agendamentos;

public static class AgendamentoMesagens
{
    /// <summary>
    /// Codigo de mensagem 5.8.x
    /// Valores de x.x.0 até x.x.10 são reservados para codigos "COMUNS"
    /// </summary>
    
    public static string ValorNulo(string nome) => $"5.8.0 - {nome} não pode ser nullo";

    public static string TempoMinimo(int minutos) => $"5.8.2 - O tempo mínimo do agendamento deve ser de {minutos} minutos";

}