using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Host.Models
{
    [Table("ordering")]
    public class Ordering
    {
        [Key]
        [ForeignKey("dish_id")]
        public Dish Dish { get; set; }

        [Key]
        [ForeignKey("order_id")]
        public Order Order { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [Column("quantity")]
        public int Quantity { get; set; } = 1;
    }
}