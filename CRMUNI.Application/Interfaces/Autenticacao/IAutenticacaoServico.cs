using HeyChefe.Application.Modelos.Autenticação;
using HeyChefe.Domain.Entidades.Segurança;

namespace HeyChefe.Application.Interfaces.Autenticação
{
    public interface IAutenticacaoServico
    {
        ResultadoToken GeraToken(string idUsuario, string email);

        string GeraRefreshToken();

        void ValidaToken(string token);

        ResultadoToken RefreshToken(Autenticacao autenticacao, string antigoRefreshToken);
    }
}
