using CRMUNI.Domain.Validacoes.Geral;

namespace CRMUNI.Domain.Validacoes.Entidades.Atendimentos;

public sealed class AtendimentoValidacao : BaseValidacao, IValidacao<AtendimentoValidacao>
{
    public AtendimentoValidacao(string erro) : base(erro)
    {
    }

    public static void Verifica(bool condicao, string mensagem)
        => VerificaExcessao<AtendimentoValidacao>(condicao, mensagem);
}