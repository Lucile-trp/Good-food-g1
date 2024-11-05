using Host.Interfaces.Repository;
using Host.Interfaces.Services;
using Host.Models;
using System.Collections.Generic;

namespace Host.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        // GET
        public ICollection<Supplier> GetSuppliers()
        {
            return _supplierRepository.GetSuppliers();
        }

        public Supplier GetSupplierById(int supplierId)
        {
            return _supplierRepository.GetSupplierById(supplierId);
        }

        // CREATE
        public bool CreateSupplier(Supplier supplier)
        {
            return _supplierRepository.CreateSupplier(supplier);
        }

        // UPDATE 
        public bool UpdateSupplier(Supplier supplier)
        {
            return _supplierRepository.UpdateSupplier(supplier);
        }

        // DELETE 
        public bool DeleteSupplier(int supplierId)
        {
            var supplier = _supplierRepository.GetSupplierById(supplierId);
            if (supplier == null)
            {
                return false;
            }
            return _supplierRepository.DeleteSupplier(supplier);
        }

        // CHECK
        public bool SupplierExists(int supplierId)
        {
            return _supplierRepository.SupplierExists(supplierId);
        }
    }
}
