using APICatalogo.App.Context;
using APICatalogo.App.Domain.Category.Entities;
using APICatalogo.App.Domain.Category.Models.Pagination;
using APICatalogo.App.Repositories;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.App.Domain.Category.Repositories;

public class CategoryRepository: Repository<CategoryEntity>, ICategoryRepository
{
    public CategoryRepository(DatabaseContext context) : base(context)
    {
    }

    public async Task<IEnumerable<CategoryEntity>> GetCategoryAndProducts()
    {
        return await  _context.Category
            .Include(c => c.Products)
            .AsNoTracking()
            .ToListAsync();
    }
    
    public async Task<PagedList<CategoryEntity>> GetAll(CategoryPaginationParameters paginationParams)
    {
        var categories = await GetAll();
        var ordenedCategories = categories
            .OrderBy(c => c.Id)
            .AsQueryable();

        var result = PagedList<CategoryEntity>.ToPagedList(
                ordenedCategories, 
                paginationParams.PageNumber, 
                paginationParams.PageSize
        );

        return result;
    }

    // public IEnumerable<CategoryEntity> GetAll(CategoryPaginationParameters paginationParams)
    // {
    //     return GetAll()
    //         .OrderBy(c => c.Id)
    //         .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
    //         .Take(paginationParams.PageSize).ToList();
    // }
}