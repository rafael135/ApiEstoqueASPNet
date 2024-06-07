using ApiEstoqueASP.Data;
using ApiEstoqueASP.Models;
using ApiEstoqueASP.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ApiEstoqueASP.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApiEstoqueDbContext context) : base(context)
        {
            
        }

        public IEnumerable<Product> Products => _context.Products.Include(pr => pr.Supplier);

        public Product CreateProduct(Product product)
        {
            EntityEntry<Product> createdEntity = _context.Products.Add(product);
            _context.SaveChanges();

            Product createdProduct = Products
                .Where(pr => pr.Id == createdEntity.Entity.Id)
                .First();

            return createdProduct;
        }

        public Product? GetProductById(int productId)
        {
            return _context.Products
                .Where(pr => pr.Id == productId)
                .Include(pr => pr.Supplier)
                //.Include(pr => pr.OrderItems)
                .FirstOrDefault();
        }

        public List<Product> GetProductsByName(string productName)
        {
            return this.Products
                .Where(pr => pr.Name.Equals(productName))
            .ToList();
        }
    }
}
