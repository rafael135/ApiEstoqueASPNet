using ApiEstoqueASP.Data.DTOs;
using ApiEstoqueASP.Models;

namespace ApiEstoqueASP.Services.Interfaces
{
    public interface IOrderService
    {
        OrderItem? GetOrderItemById(int id);
        OrderItem RegisterNewOrderItem(OrderItem orderItem);
        Order RegisterNewOrder(Order order);
        Order GetOrderInfo(int id);
        List<OrderItem> GetOrderItems(Order order);
        OrderItem? UpdateOrderItem(int id, UpdateOrderItemDto dto);
    }
}
