using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PushThenPause.Data;
using PushThenPause.Data.Models;

namespace PushThenPause.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly PushThenPauseDbContext _context;

        public UserController(PushThenPauseDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            User? user = await _context.Users
                .Include(t => t.Tasks)
                .Include(b => b.BreakActivities)
                .Include(c => c.Cycles)
                .Include(s => s.StreakTracker)
                .FirstOrDefaultAsync(u => u.UserId == id);

            return user is null ? NotFound() : Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create(User user)
        {
            User? existingUser = _context.Users
                .FirstOrDefault(u => u.Email == user.Email);

            if (existingUser is not null)
            {
                return BadRequest($"There is already an account with email: {user.Email}");
            }

            await _context.Users
                .AddAsync(user);

            return CreatedAtAction(nameof(GetById), new { id = user.UserId }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest("The ID has no relation to this user.");
            }

            User? existingUser = await _context.Users.FindAsync(user.UserId);
            if (existingUser is null)
            {
                return NotFound();
            }

            existingUser.DisplayName = user.DisplayName;
            existingUser.Email = user.Email;
            existingUser.Username = user.Username;
            existingUser.IsNemsModeEnabled = user.IsNemsModeEnabled;

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
