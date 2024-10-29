using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project1.Data;
using project1.DTOS;

namespace project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly ProjectDB _context; // Replace with your specific DbContext, e.g., ClinicContext

        public AddressesController(ProjectDB context)
        {
            _context = context;
        }

        // Create Address
        [HttpPost]
        public async Task<ActionResult<AddressDto>> AddAddress(AddressDto addressDto)
        {
            var address = new Address
            {
                Street = addressDto.Street,
                City = addressDto.City,
                State = addressDto.State,
                ZipCode = addressDto.ZipCode,
                Country = addressDto.Country,
                UserId = addressDto.UserId
            };

            _context.Set<Address>().Add(address);
            await _context.SaveChangesAsync();

            addressDto.Id = address.Id;
            return CreatedAtAction(nameof(GetAddress), new { id = addressDto.Id }, addressDto);
        }

        // Read All Addresses
        [HttpGet]
        public async Task<ActionResult<List<AddressDto>>> GetAllAddresses()
        {
            return await _context.Set<Address>()
                .Select(a => new AddressDto
                {
                    Id = a.Id,
                    Street = a.Street,
                    City = a.City,
                    State = a.State,
                    ZipCode = a.ZipCode,
                    Country = a.Country,
                    UserId = a.UserId
                })
                .ToListAsync();
        }

        // Read Address by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<AddressDto>> GetAddress(int id)
        {
            var address = await _context.Set<Address>().FindAsync(id);
            if (address == null) return NotFound();

            var addressDto = new AddressDto
            {
                Id = address.Id,
                Street = address.Street,
                City = address.City,
                State = address.State,
                ZipCode = address.ZipCode,
                Country = address.Country,
                UserId = address.UserId
            };
            return Ok(addressDto);
        }

        // Update Address
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress(int id, AddressDto addressDto)
        {
            if (id != addressDto.Id) return BadRequest("ID mismatch");

            var address = await _context.Set<Address>().FindAsync(id);
            if (address == null) return NotFound();

            address.Street = addressDto.Street;
            address.City = addressDto.City;
            address.State = addressDto.State;
            address.ZipCode = addressDto.ZipCode;
            address.Country = addressDto.Country;
            address.UserId = addressDto.UserId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Delete Address
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var address = await _context.Set<Address>().FindAsync(id);
            if (address == null) return NotFound();

            _context.Set<Address>().Remove(address);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
