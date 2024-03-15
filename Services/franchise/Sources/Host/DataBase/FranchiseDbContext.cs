using Microsoft.EntityFrameworkCore;
using Host.Models;

public class FranchiseDbContext : DbContext
{
    public FranchiseDbContext(DbContextOptions<FranchiseDbContext> options) : base(options)
    {
    }

    public DbSet<Supplier> SuppliersDb { get; set; }

}
