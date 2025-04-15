namespace PushThenPause.Data.Models;

public class Cycle
{
    public int CycleId { get; set; }
    public int UserId { get; set; }
    public int? TaskId { get; set; }
    public int? BreakActivityId { get; set; }

    public DateTime StartedAt { get; set; }
    public DateTime? EndedAt { get; set; }
    public string? Notes { get; set; }

    public User User { get; set; } = null!;
    public UserTask? Task { get; set; }
    public BreakActivity? BreakActivity { get; set; }
}
