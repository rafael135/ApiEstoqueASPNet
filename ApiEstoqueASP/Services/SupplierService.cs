using ApiEstoqueASP.Data.DTOs;
using ApiEstoqueASP.Models;
using ApiEstoqueASP.Repositories.Interfaces;
using ApiEstoqueASP.Services.Interfaces;

namespace ApiEstoqueASP.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public Supplier? GetSupplierById(int id)
        {
            Supplier? supplier = this._supplierRepository.GetById(id);
            return supplier;
        }

        public Supplier CreateNewSupplier(Supplier supplier)
        {
            this._supplierRepository.Add(supplier);
            return supplier;
        }

        public Supplier? UpdateSupplier(int id, UpdateSupplierDto dto)
        {
            Supplier? supplier = this._supplierRepository.GetById(id);

            if(supplier is null)
            {
                return null;
            }

            supplier.Name = dto.Name;
            this._supplierRepository.Update(supplier);

            return supplier;
        }
    }
}
