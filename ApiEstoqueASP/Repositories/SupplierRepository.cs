using ApiEstoqueASP.Data;
using ApiEstoqueASP.Models;
using ApiEstoqueASP.Repositories.Interfaces;

namespace ApiEstoqueASP.Repositories
{
    public class SupplierRepository : GenericRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(ApiEstoqueDbContext context) : base(context)
        {
            
        }

        
        public List<Product> GetProductsByName(string productName)
        {
            return new List<Product>();
        }
        
    }
}
