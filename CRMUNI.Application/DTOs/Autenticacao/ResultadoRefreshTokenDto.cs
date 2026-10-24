namespace CRMUNI.Application.DTOs.Autenticacao;

public record ResultadoRefreshTokenDto(
    string Token,
    DateTime Expiracao,
    string RefreshToken,
    DateTime ExpiracaoRefresh);