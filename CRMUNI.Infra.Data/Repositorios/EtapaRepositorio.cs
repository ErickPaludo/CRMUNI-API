using CRMUNI.DOMAIN.Entidades.Etapas;
using CRMUNI.DOMAIN.Repositorios;
using CRMUNI.Infra.Data.Contexto;

namespace CRMUNI.Infra.Data.Repositorios;

public class EtapaRepositorio : BaseRepositorio<Etapa>, IEtapaRepositorio
{
    public EtapaRepositorio(AppDbContext contexto) : base(contexto)
    {
    }
}