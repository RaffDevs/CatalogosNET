using APICatalogo.App.Domain.Category.Entities;
using APICatalogo.App.Domain.Category.Models.Pagination;
using APICatalogo.App.Repositories;

namespace APICatalogo.App.Domain.Category.Repositories;

public interface ICategoryRepository : IRepository<CategoryEntity>
{
    public Task<IEnumerable<CategoryEntity>> GetCategoryAndProducts();
    public Task<PagedList<CategoryEntity>> GetAll(CategoryPaginationParameters paginationParams);
}