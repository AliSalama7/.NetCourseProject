using Microsoft.AspNetCore.Mvc;
using project1.Data;

namespace project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ProjectDB _db;
        public UsersController(ProjectDB db)
        {
            _db = db;
        }

        [HttpGet("")]
        public ActionResult<List<User>> GetAll()
        {
            var users = _db.Users.ToList();
            return Ok(users);
        }

        [HttpGet("{userId}")]
        public ActionResult<User> GetById([FromRoute] int userId)
        {
            var user = _db.Users.FirstOrDefault(x => x.Id == userId);
            if (user == null)
                return NotFound("User not found");
            return Ok(user);
        }

        [HttpPost("")]
        public ActionResult<User> Create([FromBody] User newUser)
        {
            if (newUser == null)
                return BadRequest("Invalid user data");

            _db.Users.Add(newUser);
            _db.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { userId = newUser.Id }, newUser);
        }

        [HttpPut("{userId}")]
        public ActionResult Update([FromRoute] int userId, [FromBody] User updatedUser)
        {
            var user = _db.Users.FirstOrDefault(x => x.Id == userId);
            if (user == null)
                return NotFound("User not found");

            user.FName = updatedUser.FName;
            user.SName = updatedUser.SName;
            user.Email = updatedUser.Email;

            _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{userId}")]
        public ActionResult Delete([FromRoute] int userId)
        {
            var user = _db.Users.FirstOrDefault(x => x.Id == userId);
            if (user == null)
                return NotFound("User not found");

            _db.Users.Remove(user);
            _db.SaveChanges();

            return NoContent();
        }
    }
}
