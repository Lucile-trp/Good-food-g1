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
    }
}