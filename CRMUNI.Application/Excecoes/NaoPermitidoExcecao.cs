namespace CRMUNI.Application.Excecoes;

public class NaoPermitidoExcecao : Exception
{
    public NaoPermitidoExcecao(string? message) : base(message)
    {
    }
}