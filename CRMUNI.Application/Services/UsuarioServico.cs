using CRMUNI.Infra.Data.Contexto;

namespace CRMUNI.Application.Services;

public class UsuarioServico
{
    private readonly AppDbContext _contexto;

    public UsuarioServico(AppDbContext contexto)
    {
        _contexto = contexto;
    }
}
