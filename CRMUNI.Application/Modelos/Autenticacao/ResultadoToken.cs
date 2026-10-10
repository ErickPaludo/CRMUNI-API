namespace HeyChefe.Application.Modelos.Autenticação
{
    public record ResultadoToken(string token, DateTime expirationTokenFormatado, string refreshToken,long expirationRefreshToken, DateTime expirationRefreshTokenFormatado);
}
