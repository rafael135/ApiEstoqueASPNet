using ApiEstoqueASP.Models;

namespace ApiEstoqueASP.Repositories.Interfaces;

public interface ISupplierRepository : IGenericRepository<Supplier>
{
    List<Product> GetProductsByName(string productName);
}
