namespace CRMUNI.Application.Interfaces.Seguranca
{
    public interface ISegurancaServico
    {
        (string salt, string hash) CriaSenhaArgon(string senha, string? salt = null);
        bool ValidaSenhaArgon(string senhaBanco, string senha, string salt);
    }
}