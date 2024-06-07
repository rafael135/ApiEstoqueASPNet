using ApiEstoqueASP.Models;

namespace ApiEstoqueASP.Repositories.Interfaces;

public interface IProductRepository : IGenericRepository<Product>
{
    Product CreateProduct(Product product);
    List<Product> GetProductsByName(string productName);
    Product? GetProductById(int productId);
}
