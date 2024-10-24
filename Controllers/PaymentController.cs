using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project1.Data;

namespace project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ProjectDB _context;
        public PaymentController(ProjectDB context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();
                return Ok(payment);
            }
            return BadRequest(ModelState);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);

            if (payment == null)
            {
                return NotFound();
            }

            return Ok(payment);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, [FromBody] Payment updatedPayment)
        {
            if (id != updatedPayment.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var payment = await _context.Payments.FindAsync(id);

            if (payment == null)
            {
                return NotFound();
            }

            payment.Amount = updatedPayment.Amount;
            payment.PaymentMethod = updatedPayment.PaymentMethod;
            payment.PaymentDate = updatedPayment.PaymentDate;
            payment.Status = updatedPayment.Status;
            payment.OrderId = updatedPayment.OrderId;
            payment.UserId = updatedPayment.UserId;

            await _context.SaveChangesAsync();

            return Ok(payment);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);

            if (payment == null)
            {
                return NotFound();
            }

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
