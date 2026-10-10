using CRMUNI.Domain.Entidades.Etapas;
using CRMUNI.Domain.Repositorios;
using CRMUNI.Infra.Data.Contexto;

namespace CRMUNI.Infra.Data.Repositorios;

public class EtapaRepositorio : BaseRepositorio<Etapa>, IEtapaRepositorio
{
    public EtapaRepositorio(AppDbContext contexto) : base(contexto)
    {
    }
}