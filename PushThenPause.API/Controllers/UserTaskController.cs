using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PushThenPause.Data;
using PushThenPause.Data.Models;

namespace PushThenPause.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserTaskController : ControllerBase
    {
        private readonly PushThenPauseDbContext _context;
        public UserTaskController(PushThenPauseDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTask>>> GetAll()
        {
            List<UserTask> tasks = await _context.UserTasks
                .ToListAsync();

            return Ok(tasks);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<UserTask>>> GetByUserId(int userId)
        {
            List<UserTask> tasks = await _context.UserTasks
                .Where(t => t.UserId == userId)
                .ToListAsync();

            return Ok(tasks);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<UserTask>> GetById(int id)
        {
            UserTask? task = await _context.UserTasks
                .FirstOrDefaultAsync(t => t.UserTaskId == id);

            return task is null ? NotFound() : Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<UserTask>> Create([FromBody] UserTask userTask)
        {
            _context.UserTasks.Add(userTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = userTask.UserTaskId }, userTask);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserTask updatedTask)
        {
            if (id != updatedTask.UserTaskId)
            {
                return BadRequest("The ID has no relation to this task.");
            }

            UserTask? existingTask = await _context.UserTasks.FindAsync(id);
            if (existingTask is null)
            {
                return NotFound();
            }

            existingTask.Title = updatedTask.Title;
            existingTask.DurationMinutes = updatedTask.DurationMinutes;
            existingTask.Notes = updatedTask.Notes;
            existingTask.IsRecurring = updatedTask.IsRecurring;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            UserTask? existingTask = await _context.UserTasks.FindAsync(id);
            if (existingTask == null)
            {
                return NotFound();
            }

            _context.UserTasks.Remove(existingTask);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
