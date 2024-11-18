using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Host.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        [Column("id")]
        public int UserId { get; set; }

        [Required, MaxLength(100)]
        [Column("email")]
        public string Email { get; set; }

        [Required]
        [Column("password")]
        public string Password { get; set; }

        [Required, MaxLength(50)]
        [Column("first_name")]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        [Column("last_name")]
        public string LastName { get; set; }

        [Phone, MaxLength(15)]
        [Column("phone")]
        public string Phone { get; set; }

        [MaxLength(200)]
        [Column("address")]
        public string Address { get; set; }

        [MaxLength(10)]
        [Column("zip")]
        public string Zip { get; set; }

        [MaxLength(50)]
        [Column("city")]
        public string City { get; set; }

        [MaxLength(50)]
        [Column("country")]
        public string Country { get; set; }

        [InverseProperty("Customer")]
        public List<DeliveryAddress> DeliveryAddresses { get; set; } = new List<DeliveryAddress>();

        [InverseProperty("Customer")]
        public List<Order> OrdersAsCustomer { get; set; } = new List<Order>();

        [InverseProperty("Deliverer")]
        public List<Order> OrdersAsDeliverer { get; set; } = new List<Order>();
    }
}