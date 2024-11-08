using Newtonsoft.Json;

namespace Host.Dto.Rpc
{
    public class OrderingSenderDto
    {
        [JsonProperty("dishId")]
        public string DishId { get; set; }

        [JsonProperty("orderId")]
        public string OrderId { get; set; }        
    }
}