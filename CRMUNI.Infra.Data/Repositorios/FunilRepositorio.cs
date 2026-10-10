using CRMUNI.Domain.Entidades.Funis;
using CRMUNI.Domain.Repositorios;
using CRMUNI.Infra.Data.Contexto;

namespace CRMUNI.Infra.Data.Repositorios;

public class FunilRepositorio : BaseRepositorio<Funil>, IFunilRepositorio
{
    public FunilRepositorio(AppDbContext contexto) : base(contexto)
    {
    }
}