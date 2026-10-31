using CRMUNI.Domain.Entidades.Planos;
using CRMUNI.Domain.Repositorios;
using CRMUNI.Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;

namespace CRMUNI.Infra.Data.Repositorios;

public class PlanoRepositorio : BaseRepositorio<Plano>, IPlanoRepositorio
{
    private readonly AppDbContext _contexto;

    public PlanoRepositorio(AppDbContext contexto) : base(contexto)
    {
        _contexto = contexto;
    }

    public IQueryable<Plano> Retorno()
    {
        return _contexto.Set<Plano>()
            .Include(x => x.Empresa);
    }
}
