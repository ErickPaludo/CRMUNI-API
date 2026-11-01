using CRMUNI.Application.Excecoes;
using CRMUNI.Domain.Execoes;
using CRMUNI.Domain.Validacoes.Geral;
using Microsoft.AspNetCore.SignalR;

namespace CRMUNI.UI.Chat.Extensao;

/// <summary>
/// Converte excecoes de dominio/aplicacao em HubException, cuja mensagem chega ao cliente.
/// Qualquer outra excecao e registrada e devolvida como erro generico.
/// </summary>
public sealed class FiltroExcecaoHub : IHubFilter
{
    private readonly ILogger<FiltroExcecaoHub> _logger;

    public FiltroExcecaoHub(ILogger<FiltroExcecaoHub> logger)
    {
        _logger = logger;
    }

    public async ValueTask<object?> InvokeMethodAsync(
        HubInvocationContext invocationContext,
        Func<HubInvocationContext, ValueTask<object?>> next)
    {
        try
        {
            return await next(invocationContext);
        }
        catch (HubException)
        {
            throw;
        }
        catch (Exception ex) when (ex is BaseValidacao
                                       or ExceptionDomain
                                       or NaoEncontradoExecao
                                       or NaoPermitidoExcecao
                                       or NaoAutorizadoExecao
                                       or ConflitoExecao)
        {
            throw new HubException(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado no hub ao executar {Metodo}: {Mensagem}",
                invocationContext.HubMethodName, ex.Message);
            throw new HubException("Erro interno do servidor.");
        }
    }
}
