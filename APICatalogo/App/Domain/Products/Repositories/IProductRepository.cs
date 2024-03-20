using APICatalogo.App.Domain.Products.Entities;
using APICatalogo.App.Repositories;

namespace APICatalogo.App.Domain.Products.Repositories;

public interface IProductRepository : IRepository<ProductEntity>
{
    public Task<IEnumerable<ProductEntity>> GetProductsByCategory(int id);
}