using Host.Data;
using Host.Interfaces.Repository;
using Host.Models;
using System.Collections.Generic;
using System.Linq;

namespace Host.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly FranchiseDbContext _context;

        public SupplierRepository(FranchiseDbContext context)
        {
            _context = context;
        }

        // GET 
        public ICollection<Supplier> GetSuppliers()
        {
            return _context.Suppliers.ToList();
        }

        public Supplier GetSupplierById(int supplierId)
        {
            return _context.Suppliers.FirstOrDefault(da => da.SupplierId == supplierId);
        }

        // CREATE 
        public bool CreateSupplier(Supplier supplier)
        {
            _context.Add(supplier);
            return Save();
        }

        // UPDATE
        public bool UpdateSupplier(Supplier supplier)
        {
            _context.Update(supplier);
            return Save();
        }

        // DELETE 
        public bool DeleteSupplier(Supplier supplier)
        {
            _context.Remove(supplier);
            return Save();
        }

        // CHECK
        public bool SupplierExists(int supplierId)
        {
            return _context.Suppliers.Any(da => da.SupplierId == supplierId);
        }

        // SAVE
        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
