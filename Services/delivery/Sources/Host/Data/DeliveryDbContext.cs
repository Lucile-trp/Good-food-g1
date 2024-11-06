using Microsoft.EntityFrameworkCore;
using Host.Models;

namespace Host.Data
{
    public class DeliveryDbContext : DbContext
    {
        public DeliveryDbContext(DbContextOptions<DeliveryDbContext> options) : base(options)
        {
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<DeliveryAddress> DeliveryAddresses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Ordering> Orderings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ordering>()
                .HasKey(o => new { o.OrderId, o.DishId });

            modelBuilder.Entity<Ordering>()
                .HasOne(o => o.Order)
                .WithMany(o => o.Orderings)
                .HasForeignKey(o => o.OrderId);

            modelBuilder.Entity<Ordering>()
                .HasOne(o => o.Dish)
                .WithMany(d => d.Orderings)
                .HasForeignKey(o => o.DishId);
        }
    }
}
