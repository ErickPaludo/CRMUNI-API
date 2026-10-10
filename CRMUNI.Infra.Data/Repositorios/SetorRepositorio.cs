using CRMUNI.DOMAIN.Entidades.Setores;
using CRMUNI.DOMAIN.Repositorios;
using CRMUNI.Infra.Data.Contexto;

namespace CRMUNI.Infra.Data.Repositorios;

public class SetorRepositorio : BaseRepositorio<Setor>, ISetorRepositorio
{
    public SetorRepositorio(AppDbContext contexto) : base(contexto)
    {
    }
}