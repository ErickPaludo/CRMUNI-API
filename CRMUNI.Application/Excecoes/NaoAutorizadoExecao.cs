namespace CRMUNI.Application.Excecoes;

public class NaoAutorizadoExecao : Exception
{
    public NaoAutorizadoExecao(string? message) : base(message)
    {
    }
}