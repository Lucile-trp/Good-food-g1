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
        public int UserId { get; set; }

        [Required, MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Phone, MaxLength(15)]
        public string Phone { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }

        [MaxLength(10)]
        public string Zip { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(50)]
        public string Country { get; set; }

        [InverseProperty("Customer")]
        public List<DeliveryAddress> DeliveryAddresses { get; set; } = new List<DeliveryAddress>();

        [InverseProperty("Customer")]
        public List<Order> OrdersAsCustomer { get; set; } = new List<Order>();

        [InverseProperty("Deliverer")]
        public List<Order> OrdersAsDeliverer { get; set; } = new List<Order>();
    }
}