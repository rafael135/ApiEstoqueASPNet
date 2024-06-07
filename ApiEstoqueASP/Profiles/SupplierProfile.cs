using ApiEstoqueASP.Data.DTOs;
using ApiEstoqueASP.Models;
using AutoMapper;

namespace ApiEstoqueASP.Profiles;

public class SupplierProfile : Profile
{
    public SupplierProfile()
    {
        CreateMap<CreateSupplierDto, Supplier>();
        CreateMap<Supplier, ReadSupplierDto>()
            .ForMember(supplierDto => supplierDto.Products, opt => opt.MapFrom(supplier => supplier.Products))
            .ForMember(supplierDto => supplierDto.RegisterDate, opt => opt.MapFrom(supplier => supplier.RegisterDate));
        CreateMap<UpdateSupplierDto, Supplier>();
    }
}
