using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PushThenPause.Data;
using PushThenPause.Data.Models;

namespace PushThenPause.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NemsModeSettingsController : ControllerBase
    {
        private readonly PushThenPauseDbContext _context;

        public NemsModeSettingsController(PushThenPauseDbContext context)
        {
            _context = context;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<NemsModeSettings>> GetByUserId(int userId)
        {
            NemsModeSettings? nemsModeSettings = _context.NemsModeSettings
                .FirstOrDefault(x => x.UserId == userId);

            return nemsModeSettings is null ? NotFound() : Ok(nemsModeSettings);
        }

        [HttpPost]
        public async Task<ActionResult<NemsModeSettings>> Create([FromBody] NemsModeSettings nemsModeSettings)
        {
            NemsModeSettings? existingSettings = await _context.NemsModeSettings
                .FirstOrDefaultAsync(s => s.UserId == nemsModeSettings.UserId);

            if (existingSettings is not null)
            {
                return Conflict("These settings already exists for this user.");
            }

            await _context.AddAsync(nemsModeSettings);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByUserId),
                new { id = nemsModeSettings.UserId }, nemsModeSettings);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, NemsModeSettings nemsModeSettings)
        {
            if (id != nemsModeSettings.UserId)
            {
                return BadRequest("The ID has no relation to this setting.");
            }

            NemsModeSettings? existingSettings = await _context.NemsModeSettings
                .FirstOrDefaultAsync(n => n.UserId == nemsModeSettings.UserId);
            if (existingSettings is null)
            {
                return NotFound();
            }

            existingSettings.EncouragementFrequency = nemsModeSettings.EncouragementFrequency;
            existingSettings.IsEnabled = nemsModeSettings.IsEnabled;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            NemsModeSettings? settings = await _context.NemsModeSettings.FindAsync(id);
            if (settings is null)
            {
                return NotFound();
            }

            _context.Remove(settings);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
