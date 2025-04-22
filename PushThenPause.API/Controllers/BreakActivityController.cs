using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PushThenPause.Data;
using PushThenPause.Data.Models;

namespace PushThenPause.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BreakActivityController : ControllerBase
    {
        private readonly PushThenPauseDbContext _context;
        public BreakActivityController(PushThenPauseDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BreakActivity>>> GetAll()
        {
            List<BreakActivity> breakActivities = await _context.BreakActivities
                .ToListAsync();

            return Ok(breakActivities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BreakActivity>> GetById(int id)
        {
            BreakActivity? breakActivity = await _context.BreakActivities
                .FirstOrDefaultAsync(b => b.BreakActivityId == id);

            return breakActivity is null ? NotFound() : Ok(breakActivity);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<BreakActivity>>> GetByuserId(int userId)
        {
            List<BreakActivity> breakActivities = await _context.BreakActivities
                .Where(b => b.UserId == userId)
                .ToListAsync();

            return Ok(breakActivities);
        }

        [HttpPost]
        public async Task<ActionResult<BreakActivity>> Create([FromBody] BreakActivity breakActivity)
        {
            await _context.BreakActivities
                .AddAsync(breakActivity);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = breakActivity.BreakActivityId }, breakActivity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BreakActivity breakActivity)
        {
            if (id != breakActivity.BreakActivityId)
            {
                return BadRequest("The ID has no relation to this break activity.");
            }

            BreakActivity? existingBreakActivity = await _context.BreakActivities
                .FindAsync(id);
            if (existingBreakActivity is null)
            {
                return NotFound();
            }

            existingBreakActivity.DurationMinutes = breakActivity.DurationMinutes;
            existingBreakActivity.Title = breakActivity.Title;
            existingBreakActivity.UserId = breakActivity.UserId;
            existingBreakActivity.MoodTag = breakActivity.MoodTag;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            BreakActivity? existingBreakActivity = await _context.BreakActivities
                .FirstOrDefaultAsync(b => b.BreakActivityId == id);
            if (existingBreakActivity == null)
            {
                return NotFound();
            }

            _context.BreakActivities
                .Remove(existingBreakActivity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("mood/{moodTag}")]
        public async Task<ActionResult<IEnumerable<BreakActivity>>> GetByMood(string moodTag)
        {
            List<BreakActivity> breakActivities = await _context.BreakActivities
                .Where(b => b.MoodTag == moodTag)
                .ToListAsync();

            return Ok(breakActivities);
        }
    }
}
