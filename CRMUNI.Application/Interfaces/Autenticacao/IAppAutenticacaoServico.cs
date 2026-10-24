using CRMUNI.Application.DTOs.Autenticacao;
using CRMUNI.Application.Modelos.Autenticacao;

namespace CRMUNI.Application.Interfaces.Autenticacao;

public interface IAppAutenticacaoServico
{
   Task<ResultadoTokenDto> GeraToken(AutenticacaoDto dto);
   Task<ResultadoRefreshTokenDto> GeraRefreshToken(string refreshToken);
}