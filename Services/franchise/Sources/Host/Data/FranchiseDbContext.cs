using Microsoft.EntityFrameworkCore;
using Host.Models;

namespace Host.Data{

    public class FranchiseDbContext : DbContext
    {
        public FranchiseDbContext(DbContextOptions<FranchiseDbContext> options) : base(options)
        {
        }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
