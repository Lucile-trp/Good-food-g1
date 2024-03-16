using Microsoft.EntityFrameworkCore;
using Host.Models;

namespace Host.DataBase{

    public class DeliveryDbContext : DbContext
    {
        public DeliveryDbContext(DbContextOptions<DeliveryDbContext> options) : base(options)
        {
        }

        public DbSet<DeliveryAddress> DeliveryAddressDb { get; set; }

    }
}
