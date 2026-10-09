namespace CRMUNI.DOMAIN.Repositorios;

public interface IBaseRepositorio<T> where T : class
{
   void Insere(T entidade);
   void Atualiza(T entidade);
   void Remove(T entidade);
   Task<T?> IdExiste<TId>(TId id);
}