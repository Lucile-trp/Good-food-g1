using Host.Models;
using System.Collections.Generic;

namespace Host.Interfaces.Services
{
    public interface ISupplierService
    {
        ICollection<Supplier> GetSuppliers();
        Supplier GetSupplierById(int supplierId);
        bool CreateSupplier(Supplier supplier);
        bool UpdateSupplier(Supplier supplier);
        bool DeleteSupplier(int supplierId);
        bool SupplierExists(int supplierId);
    }
}
