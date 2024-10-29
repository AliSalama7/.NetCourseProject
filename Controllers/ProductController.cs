using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project1.Data;
using project1.DTOS;

namespace project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProjectDB _db;
        public ProductController(ProjectDB db)
        {
            _db = db;
        }
        [HttpGet]
        public ActionResult<List<ProductDto>> Get()
        {
            List<Product> products = _db.Products.Include(p => p.Brand)
                .Include(p => p.Category).ToList();
            List<ProductDto> itemDtos = products.Select(item => new ProductDto
            {
                Name = item.Name,
                Price = item.Price,
                Description = item.Description,
                ImageUrl = item.ImageUrl,
                StockQuantity = item.StockQuantity,
                CreatedDate = item.CreatedDate,
                ModifiedDate = item.ModifiedDate,
                Category = item.Category.Name,
                Brand = item.Brand.Name
            }).ToList();
            return Ok(itemDtos);
        }
        [HttpPost]
        public ActionResult<ProductDto> Add([FromBody] ProductDto item)
        {
            var brand = _db.Brands.FirstOrDefault
                (b => string.Equals(b.Name == item.Brand, StringComparison.OrdinalIgnoreCase));
            var category = _db.Categories.FirstOrDefault
                (c => string.Equals(c.Name, item.Category, StringComparison.OrdinalIgnoreCase));
            if (brand == null || category == null)
            {
                return BadRequest("Brand or Category are not available");
            }
            var product = new Product
            {
                Name = item.Name,
                Price = item.Price,
                Description = item.Description,
                ImageUrl = item.ImageUrl,
                StockQuantity = item.StockQuantity,
                CreatedDate = item.CreatedDate,
                BrandId = brand.Id,
                Brand = brand,
                CategoryId = category.Id,
                Category = category
            };
            _db.Products.Add(product);
            _db.SaveChanges();
            return Ok(item);
        }
        [HttpPatch("{id}")]
        public ActionResult<ProductDto> UpdatePrice(int id,decimal newPrice) 
        {
            var product = _db.Products.Include(p => p.Category).Include(p => p.Brand).FirstOrDefault(p=>p.Id == id);
            if (product == null)
                return NotFound();
            product.Price = newPrice;
            _db.SaveChanges();
            return Ok(new ProductDto
            {
                Name= product.Name,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                IsActive = product.IsActive,
                StockQuantity = product.StockQuantity,
                CreatedDate = product.CreatedDate,
                Brand = product.Brand.Name,
                Category = product.Category.Name
            });
        }
        [HttpDelete]
        public ActionResult Delete(int id) 
        {
            var product = _db.Products.Find(id);
            if(product == null)
                return NotFound();
            _db.Products.Remove(product);
            _db.SaveChanges();
            return Ok();
        }
    }
}
