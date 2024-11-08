using Host.Enums;
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
        public DateTime Date { get; set; } = DateTime.UtcNow;

        [Column("order_state")]
        public OrderState OrderState { get; set; }

        [Required]
        [ForeignKey("customer_id")]
        public User Customer { get; set; }

        [ForeignKey("deliverer_id")]
        public User? Deliverer { get; set; }

        [ForeignKey("delivery_adresss_id")]
        public DeliveryAddress DeliveryAddress { get; set; }   

        //public List<Ordering> Orderings { get; } = new List<Ordering>();
    }
}