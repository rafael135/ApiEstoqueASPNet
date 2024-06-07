using ApiEstoqueASP.Data.DTOs;
using ApiEstoqueASP.Models;
using AutoMapper;

namespace ApiEstoqueASP.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<CreateProductDto, Product>();
        CreateMap<Product, ReadProductDto>()
            .ForMember(prodDto => prodDto.Supplier, opt => opt.MapFrom(prod => prod.Supplier));
        CreateMap<UpdateProductDto, Product>();
    }
}
