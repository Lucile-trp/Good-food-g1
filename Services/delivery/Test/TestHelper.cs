using Host.Data;
using Host.Interfaces.Services;
using Host.Repository;
using Host.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class TestHelper
    {
        private readonly DeliveryDbContext deliveryDbContext;

        public TestHelper()
        {
            var builder = new DbContextOptionsBuilder<DeliveryDbContext>();
            builder.UseInMemoryDatabase(databaseName: "DeliveryDbInMemory");

            var dbContextOptions = builder.Options;
            deliveryDbContext = new DeliveryDbContext(dbContextOptions);
            // Delete existing db before creating a new one
            deliveryDbContext.Database.EnsureDeleted();
            deliveryDbContext.Database.EnsureCreated();
        }

        public IDishService GetInMemoryDishService()
        {
            var repo = new DishRepository(deliveryDbContext);
            return new DishService(repo);
        }
    }
}
