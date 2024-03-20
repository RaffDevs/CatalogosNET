using APICatalogo.App.Context;
using APICatalogo.App.Domain.Category.Repositories;
using APICatalogo.App.Domain.Products.Repositories;

namespace APICatalogo.App.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private IProductRepository _productRepository;
    private ICategoryRepository _categoryRepository;
    private readonly DatabaseContext _context;

    public UnitOfWork(DatabaseContext context)
    {
        _context = context;
    }

    public IProductRepository ProductRepository => _productRepository ?? new ProductRepository(_context);

    public ICategoryRepository CategoryRepository => _categoryRepository ?? new CategoryRepository(_context);

    public async Task Commit()
    {
       await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}