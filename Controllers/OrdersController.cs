using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project1.Data;

namespace project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ProjectDB _context;

        public OrdersController(ProjectDB context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> Get()
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.ShippingAddress)
                .Include(o => o.Payment).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Details(int? id)
        {
            if (id == null) return NotFound();

            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.ShippingAddress)
                .Include(o => o.Payment)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null) return NotFound();

            return order;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> Create(Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
            }
            return order;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Order>> Edit(int id, Order order)
        {
            if (id != order.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(order);
                await _context.SaveChangesAsync();
            }
            return order;
        }


        [HttpDelete]
        public async Task<ActionResult<Order>> Delete(int? id)
        {
            if (id == null) return NotFound();

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null) return NotFound();

            return order;
        }

    }
}
