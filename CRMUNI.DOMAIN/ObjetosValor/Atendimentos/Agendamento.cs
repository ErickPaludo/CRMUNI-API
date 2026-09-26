using CRMUNI.DOMAIN.Entidades;
using CRMUNI.DOMAIN.Validacoes.ObjetosValor.Atendimentos.Agendamentos;
using CRMUNI.DOMAIN.Validacoes.Utilitarios;

namespace CRMUNI.DOMAIN.ObjetosValor.Atendimentos;

public sealed record Agendamento
{
    private const int MinPadrao = 15;
    public DateTime Data { get; }

    private Agendamento(DateTime data, int minutos)
    {
        ValidaNulo.Verifica(data,AgendamentoMesagens.ValorNulo("Data"));
        ValidaNulo.Verifica(minutos,AgendamentoMesagens.ValorNulo("Minutos"));
        ValidaMinutos(minutos);
        Data = data.AddMinutes(minutos);
    }

    private Agendamento(DateTime data)
        => Data = data.AddMinutes(MinPadrao);

    public static Agendamento Create(DateTime data, int minutos)
        => new(data, minutos);

    public static Agendamento Create()
        => new(DateTime.UtcNow);
    
    private void ValidaMinutos(int minutos)
    => AgendamentoValidacao.Verifica(minutos < MinPadrao,AgendamentoMesagens.TempoMinimo(MinPadrao));
}