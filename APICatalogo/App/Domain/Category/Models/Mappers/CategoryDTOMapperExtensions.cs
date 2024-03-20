using APICatalogo.App.Domain.Category.Entities;
using APICatalogo.App.Domain.Category.Models.DTO;

namespace APICatalogo.App.Domain.Category.Models.Mappers;

public static class CategoryDTOMapperExtensions
{
    public static CategoryDTO? ToCategoryDTO(this CategoryEntity category)
    {
        if (category is null)
        {
            return null;
        }

        return new CategoryDTO
        {
            Id = category.Id,
            Name = category.Name,
            ImageUrl = category.ImageUrl
        };
    }

    public static CategoryEntity? ToCategoryEntity(this CategoryDTO category)
    {
        if (category is null)
        {
            return null;
        }

        return new CategoryEntity
        {
            Id = category.Id,
            Name = category.Name,
            ImageUrl = category.ImageUrl
        };
    }

    public static IEnumerable<CategoryDTO> ToCollectionCategoryDTO(this IEnumerable<CategoryEntity> categories)
    {
        if (categories is null || !categories.Any())
        {
            return new List<CategoryDTO>();
        }

        return categories.Select(category => new CategoryDTO
        {
            Id = category.Id,
            Name = category.Name,
            ImageUrl = category.ImageUrl
        }).ToList();
    }
}