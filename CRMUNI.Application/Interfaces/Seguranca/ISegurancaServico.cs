using CRMUNI.Domain.ObjetosValor.Funcionarios;

namespace CRMUNI.Application.Interfaces.Seguranca
{
    public interface ISegurancaServico
    {
        Senha ConverteSenhaParaHash(string senha, string confirmacaoSenha);
        void ValidaSenhaArgon(Senha senha,string senhaTexto);
    }
}