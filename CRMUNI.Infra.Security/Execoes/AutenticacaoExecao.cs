namespace CRMUNI.Infra.Security.Execoes;

public class AutenticacaoExecao : Exception
{
    public AutenticacaoExecao(string? message) : base(message)
    {
    }
}