using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PushThenPause.Data;
using PushThenPause.Data.Models;

namespace PushThenPause.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly PushThenPauseDbContext _context;

        public UserController(PushThenPauseDbContext context)
        {
            _context = context;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> GetByUserId(int userId)
        {
            User? user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == userId);

            return user is null ? NotFound() : Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create([FromBody] User user)
        {
            User? existingUser = _context.Users
                .FirstOrDefault(u => u.Email == user.Email);

            if (existingUser is not null)
            {
                return Conflict($"There is already an account with email: {user.Email}");
            }

            string dbPath = _context.Database.GetDbConnection().DataSource;
            Console.WriteLine($"Using DB file at: {dbPath}");

            await _context.Users
                .AddAsync(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByUserId), new { userId = user.UserId }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest("The ID has no relation to this user.");
            }

            User? existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == user.UserId);
            if (existingUser is null)
            {
                return NotFound();
            }

            existingUser.DisplayName = user.DisplayName;
            existingUser.Email = user.Email;
            existingUser.Username = user.Username;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            User? existingUser = await _context.Users
                .FindAsync(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            _context.Remove(existingUser);

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
