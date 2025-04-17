namespace PushThenPause.Data.Models;

public class Cycle
{
    public int CycleId { get; set; }
    public int UserId { get; set; }

    public int? TaskId { get; set; }
    public int? BreakActivityId { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public int DurationMinutesBreakActivity { get; set; }
    public int DurationMinutesUserTask { get; set; }
    public string? Notes { get; set; }
}
