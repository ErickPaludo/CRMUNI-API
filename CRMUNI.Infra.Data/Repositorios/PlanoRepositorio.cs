using CRMUNI.DOMAIN.Entidades.Planos;
using CRMUNI.DOMAIN.Repositorios;
using CRMUNI.Infra.Data.Contexto;

namespace CRMUNI.Infra.Data.Repositorios;

public class PlanoRepositorio : BaseRepositorio<Plano>, IPlanoRepositorio
{
    public PlanoRepositorio(AppDbContext contexto) : base(contexto)
    {
    }
}