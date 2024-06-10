using ApiEstoqueASP.Data.DTOs;
using ApiEstoqueASP.Models;

namespace ApiEstoqueASP.Services.Interfaces
{
    public interface ISupplierService
    {
        Supplier? GetSupplierById(int id);
        Supplier CreateNewSupplier(Supplier supplier);

        Supplier? UpdateSupplier(int id, UpdateSupplierDto dto);
    }
}
