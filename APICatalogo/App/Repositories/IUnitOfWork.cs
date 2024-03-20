using APICatalogo.App.Domain.Category.Repositories;
using APICatalogo.App.Domain.Products.Repositories;

namespace APICatalogo.App.Repositories;

public interface IUnitOfWork
{
    IProductRepository ProductRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    Task Commit();
}