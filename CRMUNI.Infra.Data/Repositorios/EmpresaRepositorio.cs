using CRMUNI.DOMAIN.Entidades.Empresas;
using CRMUNI.DOMAIN.Repositorios;
using CRMUNI.Infra.Data.Contexto;

namespace CRMUNI.Infra.Data.Repositorios;

public class EmpresaRepositorio : BaseRepositorio<Empresa>, IEmpresaRepositorio
{
    public EmpresaRepositorio(AppDbContext contexto) : base(contexto)
    {
    }
}