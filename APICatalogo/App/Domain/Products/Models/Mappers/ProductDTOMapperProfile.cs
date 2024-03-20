using APICatalogo.App.Domain.Products.Entities;
using APICatalogo.App.Domain.Products.Models.DTO;
using AutoMapper;

namespace APICatalogo.App.Domain.Products.Models.Mappers;

public class ProductDTOMapperProfile : Profile
{
    public ProductDTOMapperProfile()
    {
        CreateMap<ProductEntity, ProductDTO>().ReverseMap();
    }
}