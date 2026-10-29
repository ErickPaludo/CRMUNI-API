using System.Linq.Expressions;

namespace CRMUNI.Domain.Repositorios;

public interface IBaseRepositorio<T> where T : class
{
   Task Insere(T entidade);
   void Atualiza(T entidade);
   void Remove(T entidade);
   Task DeleteAll();
   Task<T?> IdExiste<TId>(TId id);
   Task<T?> BuscarObjetoUnico(Expression<Func<T, bool>> predicado);
}