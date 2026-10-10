using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CRMUNI.Infra.Seguranca.Servicos.Autenticacao
{
    public class AutenticacaoServico : IAutenticacaoServico
    {
        private readonly AutenticaoConfig _autenticacaoConfig;

        public AutenticacaoServico(IOptions<AutenticaoConfig> autenticacaoConfig)
        {
            _autenticacaoConfig = autenticacaoConfig.Value;
        }

        private DateTime GeraExpiracao() => DateTime.UtcNow.AddMinutes(_autenticacaoConfig.ExpiracaoEmMinutos);

        public ResultadoToken GeraToken(string idUsuario, string email)
        {
            DateTime expiration = GeraExpiracao();

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            byte[] key = Encoding.UTF8.GetBytes(_autenticacaoConfig.SecretKeyJWT);

            string refreshToken = GeraRefreshToken();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, idUsuario),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim("sid", refreshToken)
            };

            JwtSecurityToken token = new JwtSecurityToken(
                claims: claims,
                expires: expiration,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            );

            var tokenString = handler.WriteToken(token);

            DateTime expirationRefreshToken = DateTime.UtcNow.AddDays(_autenticacaoConfig.ExpiracaoRefreshTokenDias);

            return new ResultadoToken(tokenString, expiration, refreshToken,
                UtilAutenticacao.DateTimeInUnixTimestamp(expirationRefreshToken), expirationRefreshToken);
        }

        public string GeraRefreshToken() => UtilSeguranca.GeraBase64Aleatorios(32);

        private string CriaAssinatura(byte[] data)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_autenticacaoConfig.SecretKeyJWT));
            byte[] hash = hmac.ComputeHash(data);
            return UtilAutenticacao.Base64UrlEncode(hash);
        }

        public void ValidaToken(string token)
        {
            if (token.Split('.').Count() != 3)
                throw new Exception("Token inválido.");

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            var jwtExpiration = jwt.Claims.First(x => x.Type.Equals("exp"));
            var assignature = jwt.RawSignature;

            if (jwtExpiration is null)
                throw new Exception("Token sem data de expiração.");

            if (UtilAutenticacao.UnixTimestampInDateTime(Convert.ToInt64(jwtExpiration.Value)) < DateTime.UtcNow)
                throw new Exception("Token expirado.");

            string headerPayload = $"{jwt.RawHeader}.{jwt.RawPayload}";

            if (assignature != CriaAssinatura(Encoding.UTF8.GetBytes(headerPayload)))
                throw new Exception("Token não autorizado.");
        }

        private void ValidaRefresh(string antigoRefreshToken, string novoRefreshToken, long expirationRefresh)
        {
            if (!antigoRefreshToken.Equals(novoRefreshToken))
                throw new Exception("Refresh Token inválido.");

            if (UtilAutenticacao.UnixTimestampInDateTime(expirationRefresh) < DateTime.UtcNow)
                throw new Exception("Refresh Token expirado.");
        }

        public ResultadoToken RefreshToken(Autenticacao autenticacao, string antigoRefreshToken)
        {
            ValidaRefresh(antigoRefreshToken, autenticacao!.RefreshToken!, autenticacao.ExpirationRefresh);

            return GeraToken(autenticacao.Usuario.Id.ToString(), autenticacao.Usuario.Email.Endereco);
        }
    }
}