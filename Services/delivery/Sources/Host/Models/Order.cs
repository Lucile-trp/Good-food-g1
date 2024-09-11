using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Host.Models
{
    [Table("order")]
    public class Order
    {
        [Key]
        [Column("id")]
        public int OrderId { get; set; }

        [Required]
        [Column("date")]
        public DateTime Date { get; set; } = DateTime.Now;

    }
}