using CRMUNI.Domaidd.Validacoes.Geral;

namespace CRMUNI.Domaidd.Validacoes.ObjetosValor.Atendimentos.Agendamentos;

public sealed class AgendamentoValidacao : BaseValidacao, IValidacao<AgendamentoValidacao>
{
    public AgendamentoValidacao(string erro) : base(erro)
    {
    }

    public static void Verifica(bool condicao, string mensagem)
        => VerificaExcessao<AgendamentoValidacao>(condicao, mensagem);
}