using CRMUNI.Application.Modelos.Autenticacao;

namespace CRMUNI.Application.Interfaces.Autenticacao
{
    public interface IAutenticacaoServico
    {
        ResultadoToken GeraToken(string idUsuario, string email);

        string GeraRefreshToken();

        void ValidaToken(string token);

        ResultadoToken RefreshToken(Domain.Entidades.Autenticacoes.Autenticacao autenticacao,
            string antigoRefreshToken);
    }
}