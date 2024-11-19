namespace Host.Dto
{
    public class OrderDto
    {
        public int OrderId { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime DeliveryDate { get; set; }

        public int Quantity { get; set; }
    }
}
