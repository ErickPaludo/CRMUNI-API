using CRMUNI.Application.DTOs.Mensagens;

namespace CRMUNI.Application.Interfaces;

public interface IMensagemServico
{
    /// <summary>Grava a mensagem no atendimento, enviada pelo cliente (contato) ou pelo funcionario do atendimento.</summary>
    Task<MensagemDto> GravaMensagem(int idAtendimento, ERemetente remetente, string texto);

    /// <summary>Mensagens do atendimento em ordem de envio.</summary>
    Task<List<MensagemDto>> RetornaMensagens(int idAtendimento);
}
