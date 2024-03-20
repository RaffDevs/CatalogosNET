using System.Linq.Expressions;
using APICatalogo.App.Context;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.App.Repositories;

public class Repository<T>: IRepository<T> where T: class
{
    protected readonly DatabaseContext _context;

    public Repository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetBy(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(predicate);
    }

    public async Task<T?> GetById(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<T> Create(T data)
    {
        await _context.Set<T>().AddAsync(data);
        return data;
    }

    public T? Update(T data)
    {
        _context.Set<T>().Update(data);
        return data;
    }

    public T? Delete(T data)
    {
        _context.Set<T>().Remove(data);
        return data;
    }
}