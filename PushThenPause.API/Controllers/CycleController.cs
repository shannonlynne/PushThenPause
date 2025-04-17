using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PushThenPause.Data;
using PushThenPause.Data.Models;

namespace PushThenPause.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CycleController : ControllerBase
    {
        private readonly PushThenPauseDbContext _context;

        public CycleController(PushThenPauseDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cycle>> GetById(int id)
        {
            Cycle? cycle = await _context.Cycles
                .Where(b => b.CycleId == id)
                .FirstOrDefaultAsync();

            return cycle == null ? NotFound() : Ok(cycle);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Cycle>>> GetByUser(int userId)
        {
            List<Cycle> cycles = await _context.Cycles
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.Created)
                .ToListAsync();

            return Ok(cycles);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Cycle>>> Create(Cycle cycle)
        {
            await _context.Cycles
                .AddAsync(cycle);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = cycle.CycleId }, cycle);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Cycle cycle)
        {
            if (id != cycle.CycleId)
            {
                return BadRequest("The ID has no relation to the cycle.");
            }

            Cycle? existingCycle = await _context.Cycles
                .FindAsync(id);
            if (existingCycle == null)
            {
                return NotFound();
            }

            existingCycle.DurationMinutesBreakActivity = cycle.DurationMinutesBreakActivity;
            existingCycle.DurationMinutesUserTask = cycle.DurationMinutesUserTask;
            existingCycle.BreakActivityId = cycle.BreakActivityId;
            existingCycle.TaskId = cycle.TaskId;
            existingCycle.UserId = cycle.UserId;
            existingCycle.Notes = cycle.Notes;

            await _context.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Cycle? existingCycle = await _context.Cycles
                .FirstOrDefaultAsync(c => c.CycleId == id);

            if (existingCycle == null)
            {
                return NotFound();
            }

            _context.Cycles
                .Remove(existingCycle);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
