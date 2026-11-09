using CRMUNI.Application.DTOs.Mensagens;

namespace CRMUNI.Application.Interfaces;

public interface IMensagemServico
{
    /// <summary>Grava a mensagem no atendimento, enviada pelo cliente (contato) ou pelo funcionario do atendimento.
    /// O atendimento precisa pertencer ao funcionario informado.</summary>
    Task<MensagemDto> GravaMensagem(int idAtendimento, int idFuncionario, ERemetente remetente, string texto);

    /// <summary>Mensagens do atendimento em ordem de envio. O atendimento precisa pertencer ao funcionario informado.</summary>
    Task<List<MensagemDto>> RetornaMensagens(int idAtendimento, int idFuncionario);
}
