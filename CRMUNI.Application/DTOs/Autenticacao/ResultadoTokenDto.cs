namespace CRMUNI.Application.Modelos.Autenticacao
{
    public record ResultadoTokenDto(
        string token,
        DateTime expirationTokenFormatado,
        string refreshToken,
        long expirationRefreshToken,
        DateTime expirationRefreshTokenFormatado
    );
}