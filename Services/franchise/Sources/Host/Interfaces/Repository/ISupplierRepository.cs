using Host.Models;
using System.Collections.Generic;

namespace Host.Interfaces.Repository
{
    public interface ISupplierRepository
    {
        ICollection<Supplier> GetSuppliers();
        Supplier GetSupplierById(int supplierId);
        bool CreateSupplier(Supplier supplier);
        bool UpdateSupplier(Supplier supplier);
        bool DeleteSupplier(Supplier supplier);
        bool SupplierExists(int supplierId);
        bool Save();
    }
}
