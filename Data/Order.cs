using System.ComponentModel.DataAnnotations.Schema;

namespace project1.Data
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public bool IsShipped { get; set; }
        public decimal TotalAmount { get; set; }

        // Foreign Keys
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address ShippingAddress { get; set; }

        [ForeignKey("Payment")]
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }

        public List<CartItem> OrderItems { get; set; }
    }
}
