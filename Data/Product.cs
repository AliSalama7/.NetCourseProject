using System.ComponentModel.DataAnnotations.Schema;

namespace project1.Data
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl {  get; set; }
        public int StockQuantity {  get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsActive {  get; set; }
        public decimal? Discount {  get; set; }
        [ForeignKey("Category")]
        public int CategoryId {  get; set; }
        public Category Category { get; set; }
        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }

    }
}
