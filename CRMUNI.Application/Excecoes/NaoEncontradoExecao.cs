namespace CRMUNI.Application.Excecoes;

public class NaoEncontradoExecao : Exception
{
    public NaoEncontradoExecao(string? message) : base(message)
    {
    }
}