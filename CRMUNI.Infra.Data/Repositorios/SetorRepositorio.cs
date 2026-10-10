using CRMUNI.Domain.Entidades.Setores;
using CRMUNI.Domain.Repositorios;
using CRMUNI.Infra.Data.Contexto;

namespace CRMUNI.Infra.Data.Repositorios;

public class SetorRepositorio : BaseRepositorio<Setor>, ISetorRepositorio
{
    public SetorRepositorio(AppDbContext contexto) : base(contexto)
    {
    }
}