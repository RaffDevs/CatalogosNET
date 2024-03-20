using APICatalogo.App.Domain.Category.Entities;
using APICatalogo.App.Domain.Category.Models.DTO;
using AutoMapper;

namespace APICatalogo.App.Domain.Category.Models.Mappers;

public class CategoryDTOMapperProfile : Profile
{
    public CategoryDTOMapperProfile()
    {
        CreateMap<CategoryEntity, CategoryDTO>().ReverseMap();
    }
}