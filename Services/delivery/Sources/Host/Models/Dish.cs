using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Host.Models
{
    [Table("dish")]
    public class Dish
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("title")]
        public string Title { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Required]
        [Column("cost")]
        public double Cost { get; set; }

        [Column("restaurant_id")]
        public int RestaurantId { get; set; }
    }
}