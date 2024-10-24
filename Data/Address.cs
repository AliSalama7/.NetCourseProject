using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace project1.Data
{
    public class Address : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Street { get; set; } // The street

        [Required]
        [StringLength(50)]
        public string City { get; set; } // The city

        [Required]
        [StringLength(50)]
        public string State { get; set; } // State

        [Required]
        [StringLength(20)]
        public string ZipCode { get; set; } // zip code

        [Required]
        [StringLength(50)]
        public string Country { get; set; } // Country

        // Foreign Key for User
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; } // Relationship with User
    }
}
