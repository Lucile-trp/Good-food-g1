using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Host.Models
{
    [Table("ordering")]
    public class Ordering
    {
        [Column("dish_id")]
        public int DishId { get; set; }
        public Dish Dish { get; set; }

        [Column("order_id")]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [Column("quantity")]
        public int Quantity { get; set; } = 1;
    }
}
