using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Host.Models
{
    [Table("Suppliers")]
    public class Supplier
    {
        [Key]
        [Column("id")]
        public int SupplierId { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Column("email")]
        public string Email { get; set; }

        [Required]
        [MaxLength(10)]
        [Column("phone")]
        public string Phone { get; set; }

        [Required]
        [MaxLength(200)]
        [Column("address")]
        public string Address { get; set; }

        [Required]
        [MaxLength(5)]
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
    }
}
