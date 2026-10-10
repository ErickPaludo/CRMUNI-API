namespace CRMUNI.Domain.Entidades.Segurancas
{
    public class Autenticacao
    {
        public string IdSession { get; private set; }
        public string RefreshToken { get; private set; }
        public long ExpirationRefresh { get; private set; }
        public bool Revoke { get; private set; } = false;

        public Usuario Usuario { get; private set; }

        public Autenticacao() { }

        public Autenticacao(Usuario usuario, string refreshToken, long expirationRefresh)
        {
            ValidaNulo.Verifica(usuario, MensagensBase.USUARIO_NULO);
            ValidaNulo.Verifica(refreshToken, MensagensBase.REFRESH_TOKEN_NULO);
            ValidaNulo.Verifica(expirationRefresh, MensagensBase.EXPIRATION_REFRESH_NULO);
            IdSession = Guid.CreateVersion7().ToString();
            Usuario = usuario;
            RefreshToken = refreshToken;
            ExpirationRefresh = expirationRefresh;
        }
        public void AtualizaRefreshToken(string refreshToken, long expirationRefresh)
        {
            ValidaNulo.Verifica(refreshToken, MensagensBase.REFRESH_TOKEN_NULO);
            ValidaNulo.Verifica(expirationRefresh, MensagensBase.EXPIRATION_REFRESH_NULO);

            RefreshToken = refreshToken;
            ExpirationRefresh = expirationRefresh;
            Revoke = false;
        }
        public void ValidaRefreshToken(string refreshToken)
        {
            AutenticacaoValidacao.Verifica(string.IsNullOrEmpty(refreshToken) ||
        RefreshToken is null || Revoke, MensagensAutenticacao.REFRESH_TOKEN_INVALIDO);
        }
        public void RevokaToken()
        {
            Revoke = true;
        }

    }
}
