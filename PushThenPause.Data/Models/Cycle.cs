namespace PushThenPause.Data.Models;

public class Cycle
{
    public int CycleId { get; set; }
    public int UserId { get; set; }

    public int UserTaskId { get; set; }
    public int BreakActivityId { get; set; }
    public DateOnly Created { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    public string? Notes { get; set; }
}
