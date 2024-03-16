using Microsoft.EntityFrameworkCore;
using Host.Models;

namespace Host.DataBase{

    public class FranchiseDbContext : DbContext
    {
        public FranchiseDbContext(DbContextOptions<FranchiseDbContext> options) : base(options)
        {
        }

        public DbSet<Supplier> SuppliersDb { get; set; }

    }
}
