


using System.Linq.Expressions;

namespace Uneddit.Repository;

public interface IRepository<T>
{
    void Add(T obj);
    void Update(T obj);
    void Delete(T obj);
    List<T> Filter(Expression<Func<T,bool>> exp);
}