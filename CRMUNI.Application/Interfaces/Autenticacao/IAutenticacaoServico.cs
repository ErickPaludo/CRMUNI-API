using CRMUNI.Application.Modelos.Autenticacao;

namespace CRMUNI.Application.Interfaces.Autenticacao
{
    public interface IAutenticacaoServico
    {
        ResultadoTokenDto GeraToken(string idUsuario, string email);

        string GeraRefreshToken();

        void ValidaToken(string token);

        ResultadoTokenDto RefreshToken(Domain.Entidades.Autenticacoes.Autenticacao autenticacao,
            string antigoRefreshToken);
    }
}