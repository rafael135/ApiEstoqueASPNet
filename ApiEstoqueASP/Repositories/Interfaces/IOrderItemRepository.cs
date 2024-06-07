using ApiEstoqueASP.Models;

namespace ApiEstoqueASP.Repositories.Interfaces
{
    public interface IOrderItemRepository : IGenericRepository<OrderItem>
    {
        public OrderItem? GetOrderItemById(int id);
    }
}
