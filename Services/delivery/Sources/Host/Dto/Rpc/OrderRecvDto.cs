using Newtonsoft.Json;

namespace Host.Dto.Rpc
{
    public class OrderRecvDto
    {
        [JsonProperty("dishes")]
        public DishDto[] Dishes { get; set; }

        [JsonProperty("customerId")]
        public int CustomerId { get; set; }

        [JsonProperty("deliveryId")]
        public int DeliveryId { get; set; }
        
        [JsonProperty("deliveryAdresseId")]
        public int DeliveryAdresseId { get; set; }
    }
}