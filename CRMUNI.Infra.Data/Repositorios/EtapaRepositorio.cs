using CRMUNI.Domain.Entidades.Etapas;
using CRMUNI.Domain.Repositorios;
using CRMUNI.Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;

namespace CRMUNI.Infra.Data.Repositorios;

public class EtapaRepositorio : BaseRepositorio<Etapa>, IEtapaRepositorio
{
    private readonly AppDbContext _contexto;

    public EtapaRepositorio(AppDbContext contexto) : base(contexto)
    {
        _contexto = contexto;
    }

    public IQueryable<Etapa> Retorno()
    {
        return _contexto.Set<Etapa>()
            .Include(x => x.Funil)
            .ThenInclude(x => x.Empresa);
    }
}
