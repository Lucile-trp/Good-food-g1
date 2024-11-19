namespace Host.Dto
{
    public class DeliveryAddressDto
    {
        public int DeliveryAddressId { get; set; }

        public string Address { get; set; }

        public string Zip { get; set; }
        
        public string City { get; set; }

        public string Country { get; set; }
    }
}
