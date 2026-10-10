using CRMUNI.Domain.Entidades.Usuarios;
using CRMUNI.Domain.Repositorios;
using CRMUNI.Infra.Data.Contexto;

namespace CRMUNI.Infra.Data.Repositorios;

public class UsuarioRepositorio : BaseRepositorio<Usuario>, IUsuarioRepositorio
{
    public UsuarioRepositorio(AppDbContext contexto) : base(contexto)
    {
    }
}