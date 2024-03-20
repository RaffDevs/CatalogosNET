using System.Linq.Expressions;

namespace APICatalogo.App.Repositories;

public interface IRepository<T>
{
    public Task<IEnumerable<T>> GetAll();
    public Task<T?> GetBy(Expression<Func<T, bool>> predicate);
    public Task<T?> GetById(int id);
    public Task<T> Create(T data);
    public T? Update(T data);
    public T? Delete(T data);

}