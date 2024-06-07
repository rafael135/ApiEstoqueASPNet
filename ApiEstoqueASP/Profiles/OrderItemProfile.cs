using ApiEstoqueASP.Data.DTOs;
using ApiEstoqueASP.Models;
using AutoMapper;

namespace ApiEstoqueASP.Profiles
{
    public class OrderItemProfile : Profile
    {
        public OrderItemProfile()
        {
            CreateMap<CreateOrderItemDto, OrderItem>();
            CreateMap<OrderItem, ReadOrderItemDto>();
        }
    }
}
