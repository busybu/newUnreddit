using System.Linq.Expressions;

namespace Reddit.Repository;

public interface IRepository<T>
{
    Task Add(T obj);
    Task Update(T obj);
    Task Delete(T obj);
    Task<List<T>> Filter(Expression<Func<T,bool>> exp);
}