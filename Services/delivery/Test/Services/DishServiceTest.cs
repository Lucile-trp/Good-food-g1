using FluentAssertions;
using Host.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Services
{
    public class DishServiceTest
    {
        [Fact]
        public void Create_Dish_Test()
        {
            // Arrange
            var helper = new TestHelper();
            var repo = helper.GetInMemoryDishService();

            var date = DateTime.UtcNow;
            var order = new Order
            {
                Date = date,
                OrderState = Host.Enums.OrderState.Waiting,
            };

            var dish = new Dish
            {
                Title = "Test",
                Description = "Test desc",
                Cost = 10.25,
                RestaurantId = 1,
                Order = order,
            };

            // Act
            repo.CreateDish(dish);


            // Assert
            var result = repo.GetDishes();

            // Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(1);

            result.ElementAt(0).Title.Should().Be("Test");
            result.ElementAt(0).Description.Should().Be("Test desc");
            result.ElementAt(0).Cost.Should().Be(10.25);
            result.ElementAt(0).RestaurantId.Should().Be(1);

            result.ElementAt(0).Order.Date.Should().Be(date);
            result.ElementAt(0).Order.OrderState.Should().Be(Host.Enums.OrderState.Waiting);
        }
    }
}
