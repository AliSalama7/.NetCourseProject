using Microsoft.AspNetCore.Mvc;
using project1.Data;

namespace project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartIemController : ControllerBase
    {
        private readonly ProjectDB _context;

        public CartIemController(ProjectDB context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCartItems([FromQuery] int userId)
        {
            var cartItems = _context.CartItems.Where(item => item.User.Id == userId).ToList();

            return Ok(cartItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getCartItemById(Guid id)
        {
            var cartItems = _context.CartItems.Where(item => item.Id == id).ToList();
            return Ok(cartItems);
        }


        [HttpPost]
        public async Task<IActionResult> PostCartItem([FromQuery] int productId, [FromQuery] int userId)
        {
            var user = _context.Users.FirstOrDefault(user => user.Id == userId);
            var product = _context.Products.FirstOrDefault(product => product.Id == productId);
            var cartitemAdded = _context.CartItems.Where(cart => cart.Product == product).Where(cart => cart.User.Id == userId).FirstOrDefault();
            if (user == null)
            {
                return BadRequest("no user found");
            }
            if (product == null)
            {
                return BadRequest("product not found");
            }

            if (cartitemAdded is not null)
            {
                cartitemAdded.quantity += 1;
                await _context.SaveChangesAsync();
                return Ok(cartitemAdded);
            }
            CartItem cartItem = new CartItem();
            cartItem.Product = product;
            cartItem.User = user;
            cartItem.quantity = 1;
            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
            return Ok(productId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteCartItem(Guid id)
        { 
            var cartItem = _context.CartItems.Where(item => item.Id == id).FirstOrDefault();
            if (cartItem == null)
            {
                return NotFound();
            }
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            return Ok("item has been removed");
        }
    }
}
