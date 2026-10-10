using CRMUNI.Domain.Entidades.Empresas;
using CRMUNI.Domain.Repositorios;
using CRMUNI.Infra.Data.Contexto;

namespace CRMUNI.Infra.Data.Repositorios;

public class EmpresaRepositorio : BaseRepositorio<Empresa>, IEmpresaRepositorio
{
    public EmpresaRepositorio(AppDbContext contexto) : base(contexto)
    {
    }
}