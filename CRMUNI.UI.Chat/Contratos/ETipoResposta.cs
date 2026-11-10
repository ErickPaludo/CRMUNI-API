namespace CRMUNI.UI.Chat.Contratos;

public enum ETipoResposta
{
    /// <summary>Contato ja possui atendimento em andamento; IdAtendimento preenchido.</summary>
    AtendimentoEmAndamento,

    /// <summary>Cliente deve escolher o setor e chamar SelecionarSetor.</summary>
    SelecionarSetor,

    /// <summary>Atendimento criado apos a escolha do setor; IdAtendimento preenchido.</summary>
    AtendimentoAberto,

    /// <summary>Cliente deve informar nome e sobrenome e chamar InformarNome.</summary>
    InformarNome
}
