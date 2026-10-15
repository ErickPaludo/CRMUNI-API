namespace CRMUNI.Application.Interfaces.Seguranca
{
    public interface ISegurancaServico
    {
        (string salt, string hash) CriaSenhaArgon(string senha, string confirmacaoSenha,string? salt = null);
        void ValidaSenhaArgon(string senhaBanco, string senha, string salt);
    }
}