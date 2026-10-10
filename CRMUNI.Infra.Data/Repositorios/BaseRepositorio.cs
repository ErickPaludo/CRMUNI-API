using System.Linq.Expressions;
using CRMUNI.Domain.Repositorios;
using CRMUNI.Infra.Data.Contexto;

namespace CRMUNI.Infra.Data.Repositorios;

public class BaseRepositorio<T> : IBaseRepositorio<T> where T : class
{
    private readonly AppDbContext _contexto;

    public BaseRepositorio(AppDbContext contexto)
    {
        _contexto = contexto;
    }

    public async Task Insere(T entidade)
        => await _contexto.Set<T>().AddAsync(entidade);

    public void Atualiza(T entidade)
        => _contexto.Set<T>().Update(entidade);


    public void Remove(T entidade)
        => _contexto.Set<T>().Remove(entidade);

    public async Task<T?> IdExiste<TId>(TId id)
        => await _contexto.Set<T>().FindAsync(id);
}