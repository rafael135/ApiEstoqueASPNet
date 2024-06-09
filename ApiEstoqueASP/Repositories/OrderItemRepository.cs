using ApiEstoqueASP.Data;
using ApiEstoqueASP.Models;
using ApiEstoqueASP.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoqueASP.Repositories
{
    public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
    {
        private readonly IProductRepository _productRepository;
        public OrderItemRepository(ApiEstoqueDbContext context, IProductRepository productRepository) : base(context)
        {
            this._productRepository = productRepository;
        }


        public override OrderItem Add(OrderItem entity)
        {
            Product product = this._productRepository.GetProductById(entity.ProductId);

            if (product is null || product.InStock - entity.Quantity < 0)
            {
                return null;
            }

            product.InStock -= entity.Quantity;
            this._productRepository.Update(product);

            _context.OrderItems.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public OrderItem? GetOrderItemById(int id)
        {
            return _context.OrderItems
                .Where(or => or.Id == id)
                .Include(or => or.Product)
                .Include(or => or.Order)
                .FirstOrDefault();
        }
    }
}
