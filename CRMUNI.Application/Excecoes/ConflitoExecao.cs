namespace CRMUNI.Application.Excecoes;

public class ConflitoExecao : Exception
{
    public ConflitoExecao(string? message) : base(message)
    {
    }
}