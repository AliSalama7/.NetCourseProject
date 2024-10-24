namespace project1.Data
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public decimal? quantity { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
