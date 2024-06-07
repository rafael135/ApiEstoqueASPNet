using ApiEstoqueASP.Data;
using ApiEstoqueASP.Models;
using ApiEstoqueASP.Repositories.Interfaces;

namespace ApiEstoqueASP.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApiEstoqueDbContext context) : base(context)
        {

        }

        
    }
}
