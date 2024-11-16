using System;
using Host.Enums;

namespace Host.Dto
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public OrderState OrderState { get; set; }
        public UserDto Customer { get; set; }
        public UserDto Deliverer { get; set; }
        public DeliveryAddressDto DeliveryAddress { get; set; }
        public IEnumerable<DishDto> Dishes { get; set; }
    }
}