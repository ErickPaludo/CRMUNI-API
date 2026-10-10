using CRMUNI.Domain.Entidades.Contatos;
using CRMUNI.Domain.Entidades.EntidadesBase;
using CRMUNI.Domain.Entidades.Usuarios;
using CRMUNI.Domain.Validacoes.Entidades.Autenticacoes;
using CRMUNI.Domain.Validacoes.Utilitarios;

namespace CRMUNI.Domain.Entidades.Autenticacoes
{
    public class Autenticacao : EntidadeIdGuid
    {
        public string RefreshToken { get; private set; }
        public long ExpirationRefresh { get; private set; }
        public bool Revoke { get; private set; } = false;
        public Usuario? Usuario { get; private set; }
        public Contato? Contato { get; private set; }

        public Autenticacao()
        {
        }

        public Autenticacao(Usuario usuario, string refreshToken, long expirationRefresh)
        {
            ValidaNulo.Verifica(usuario, AutenticacaoMensagens.PropriedadeNula("Usuario"));
            ValidaNulo.Verifica(refreshToken, AutenticacaoMensagens.PropriedadeNula("RefreshToken"));
            ValidaNulo.Verifica(expirationRefresh, AutenticacaoMensagens.PropriedadeNula("ExpirationRefresh"));
            Usuario = usuario;
            RefreshToken = refreshToken;
            ExpirationRefresh = expirationRefresh;
        }
        
        public Autenticacao(Contato contato, string refreshToken, long expirationRefresh)
        {
            ValidaNulo.Verifica(contato, AutenticacaoMensagens.PropriedadeNula("Contato"));
            ValidaNulo.Verifica(refreshToken, AutenticacaoMensagens.PropriedadeNula("RefreshToken"));
            ValidaNulo.Verifica(expirationRefresh, AutenticacaoMensagens.PropriedadeNula("ExpirationRefresh"));
            Contato = contato;
            RefreshToken = refreshToken;
            ExpirationRefresh = expirationRefresh;
        }

        public void AtualizaRefreshToken(string refreshToken, long expirationRefresh)
        {
            ValidaNulo.Verifica(refreshToken, AutenticacaoMensagens.PropriedadeNula("RefreshToken"));
            ValidaNulo.Verifica(expirationRefresh, AutenticacaoMensagens.PropriedadeNula("ExpirationRefresh"));

            RefreshToken = refreshToken;
            ExpirationRefresh = expirationRefresh;
            Revoke = false;
        }

        public void ValidaRefreshToken(string refreshToken)
        {
            ValidaNulo.Verifica(refreshToken, AutenticacaoMensagens.PropriedadeNula("RefreshToken"));

            AutenticacaoValidacao.Verifica(string.IsNullOrEmpty(refreshToken) || Revoke,
                AutenticacaoMensagens.RefreshTokenInvalido);
        }

        public void RevokaToken()
            => Revoke = true;
    }
}