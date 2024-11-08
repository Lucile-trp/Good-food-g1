using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Host.Models
{
    [Table("delivery_address")]
    public class DeliveryAddress
    {
        [Key]
        [Column("id")]
        public int DeliveryAddressId { get; set; }

        [Required]
        [MaxLength(200)]
        [Column("address")]
        public string Address { get; set; }

        [Required]
        [MaxLength(10)]
        [Column("zip")]
        public string Zip { get; set; }
        
        [Required]
        [MaxLength(40)]
        [Column("city")]
        public string City { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("country")]
        public string Country { get; set; }

        [Required]
        [ForeignKey("customer_id")]
        public User Customer { get; set; }
        
        [InverseProperty("DeliveryAddress")]
        public List<Order> Orders { get; } = new List<Order>();
    }
}
