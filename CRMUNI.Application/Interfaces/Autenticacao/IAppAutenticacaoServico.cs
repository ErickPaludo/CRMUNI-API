using CRMUNI.Application.DTOs.Autenticacao;
using CRMUNI.Application.Modelos.Autenticacao;

namespace CRMUNI.Application.Interfaces.Autenticacao;

public interface IAppAutenticacaoServico
{
   Task<ResultadoToken> GeraToken(AutenticacaoDto dto);
}