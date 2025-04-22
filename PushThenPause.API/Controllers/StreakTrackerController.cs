using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PushThenPause.Data;
using PushThenPause.Data.Models;

namespace PushThenPause.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StreakTrackerController : ControllerBase
    {
        private readonly PushThenPauseDbContext _context;

        public StreakTrackerController(PushThenPauseDbContext context)
        {
            _context = context;
        }

        [HttpGet("user/{userId}/today")]
        public async Task<ActionResult<StreakTracker>> GetToday(int userId)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow);
            StreakTracker? streak = await _context.StreakTrackers
                .FirstOrDefaultAsync(s => s.UserId == userId && s.Date == today);

            return streak is null ? NotFound() : Ok(streak);
        }

        [HttpPost("user/{userId}")]
        public async Task<ActionResult<StreakTracker>> Create(int userId)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow);

            StreakTracker? existing = await _context.StreakTrackers
                .FirstOrDefaultAsync(s => s.UserId == userId && s.Date == today);
            if (existing != null)
                return Conflict("Today's streak tracker already exists.");

            StreakTracker streak = new StreakTracker
            {
                UserId = userId,
                Date = today,
                StreakCount = 0
            };

            _context.StreakTrackers.Add(streak);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetToday),
                new { userId = streak.UserId },
                streak
            );
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Increment(int id)
        {
            StreakTracker? streak = await _context.StreakTrackers.FindAsync(id);
            if (streak == null)
                return NotFound();

            DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow);

            if (streak.Date < today)
            {
                streak.StreakCount = 0;
                streak.Date = today;
            }

            streak.StreakCount++;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(int userId)
        {
            StreakTracker? streak = await _context.StreakTrackers
                .FirstOrDefaultAsync(x => x.UserId == userId);
            if (streak == null)
                return NotFound();

            _context.StreakTrackers.Remove(streak);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
