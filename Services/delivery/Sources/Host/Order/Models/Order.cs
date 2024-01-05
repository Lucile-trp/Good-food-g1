using Host.Core.Models;

namespace Host.Order.Models
{
    public class Order : ModelBase
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
    }
}
