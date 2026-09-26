using CRMUNI.DOMAIN.Validacoes.Geral;

namespace CRMUNI.DOMAIN.Validacoes.ObjetosValor.Atendimentos.Agendamentos;

public sealed class AgendamentoValidacao : BaseValidacao, IValidacao<AgendamentoValidacao>
{
    public AgendamentoValidacao(string erro) : base(erro)
    {
    }

    public static void Verifica(bool condicao, string mensagem)
        => VerificaExcessao<AgendamentoValidacao>(condicao, mensagem);
}