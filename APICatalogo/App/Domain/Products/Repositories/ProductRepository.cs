using System.Linq.Expressions;
using APICatalogo.App.Context;
using APICatalogo.App.Domain.Products.Entities;
using APICatalogo.App.Domain.Products.Repositories;
using APICatalogo.App.Repositories;


namespace APICatalogo.App.Domain.Products.Repositories;

public class ProductRepository: Repository<ProductEntity>, IProductRepository
{
    public ProductRepository(DatabaseContext context) : base(context)
    {
        
    }

    public Task<IEnumerable<ProductEntity>> GetProductsByCategory(int id)
    {
        throw new NotImplementedException();
    }
}