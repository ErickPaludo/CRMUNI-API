namespace CRMUNI.Application.Modelos.Autenticacao
{
    public record ResultadoToken(
        string token,
        DateTime expirationTokenFormatado,
        string refreshToken,
        long expirationRefreshToken,
        DateTime expirationRefreshTokenFormatado
    );
}