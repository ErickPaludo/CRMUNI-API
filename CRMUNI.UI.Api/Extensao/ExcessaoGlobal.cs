using CRMUNI.Application.Excecoes;
using CRMUNI.Domain.Validacoes.Entidades.Autenticacoes;
using CRMUNI.Domain.Validacoes.Entidades.Empresas;
using CRMUNI.Domain.Validacoes.Entidades.Etapas;
using CRMUNI.Domain.Validacoes.Entidades.Funis;
using CRMUNI.Domain.Validacoes.Entidades.Setores;
using CRMUNI.Domain.Validacoes.Entidades.Usuarios;
using CRMUNI.Domain.Validacoes.ObjetosValor.Emails;
using CRMUNI.Domain.Validacoes.ObjetosValor.Nomes;
using CRMUNI.Domain.Validacoes.ObjetosValor.Telefones;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CRMUNI.UI.Api.Extensao
{
    public class ExcessaoGlobal : IExceptionHandler
    {
        private readonly ILogger<ExcessaoGlobal> _logger;

        public ExcessaoGlobal(ILogger<ExcessaoGlobal> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var (status, titulo) = exception switch
            {
                UsuarioValidacao => (400, exception.Message),
                EmpresaValidacao => (400, exception.Message),
                
                
                NomeValidacao => (400, exception.Message),
                EmailValidacao => (400, exception.Message),
                TelefoneValidacao => (400, exception.Message),
                SetorValidacao => (400, exception.Message),
                FunilValidacao => (400, exception.Message),
                EtapaValidacao => (400, exception.Message),
                
                
                
                AutenticacaoValidacao => (401, exception.Message),
                NaoAutorizadoExecao => (401, exception.Message),
                NaoPermitidoExcecao => (403, exception.Message),
                
                ConflitoExecao => (409, exception.Message),
                
                NaoEncontradoExecao => (404, exception.Message),

                KeyNotFoundException =>
                    (404, "Recurso não encontrado"),

                InvalidOperationException =>
                    (409, "Conflito de operação"),

                ArgumentException =>
                    (400, "Requisição inválida"),

                _ =>
                    (500, "Erro interno do servidor")
            };

            if (status == 500)
            {
                _logger.LogError(
                    exception,
                    "Erro inesperado: {Mensagem}",
                    exception.Message);
            }
            else
            {
                _logger.LogWarning(
                    "Exceção tratada [{Status}]: {Mensagem}",
                    status,
                    exception.Message);
            }

            // Monta a resposta ProblemDetails
            var problem = new ProblemDetails
            {
                Status = status,
                Title = titulo,
                Instance = httpContext.Request.Path
            };

            httpContext.Response.ContentType = "application/problem+json";
            httpContext.Response.StatusCode = status;

            await httpContext.Response.WriteAsJsonAsync(
                problem,
                cancellationToken);

            return true;
        }
    }
}
