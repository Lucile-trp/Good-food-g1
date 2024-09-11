using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Host.Enums;

namespace Host.Models
{
    [Table("order")]
    public class Order
    {
        [Key]
        [Column("id")]
        public int OrderId { get; set; }

        [Column("prix")]
        public decimal Price { get; set; }

        [Column("date_saisi")]
        public DateTime CreatedDate { get; set; }

        [Column("date_delivery")]
        public DateTime DeliveryDate { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }
        
        [Column("product_type")]
        public ProductType ProductType { get; set; }

        [ForeignKey("supplier_id")]
        public Supplier Supplier { get; set; }   
    }
}