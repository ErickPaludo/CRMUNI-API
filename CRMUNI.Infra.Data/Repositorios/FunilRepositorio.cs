using CRMUNI.Domain.Entidades.Funis;
using CRMUNI.Domain.Repositorios;
using CRMUNI.Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;

namespace CRMUNI.Infra.Data.Repositorios;

public class FunilRepositorio : BaseRepositorio<Funil>, IFunilRepositorio
{
    private readonly AppDbContext _contexto;

    public FunilRepositorio(AppDbContext contexto) : base(contexto)
    {
        _contexto = contexto;
    }

    public IQueryable<Funil> Retorno()
    {
        return _contexto.Funis
            .Include(x => x.Empresa);
    }
}
