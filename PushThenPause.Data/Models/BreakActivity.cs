namespace PushThenPause.Data.Models;

public class BreakActivity
{
    public int BreakActivityId { get; set; }
    public int UserId { get; set; }

    public string Title { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }
    public string? MoodTag { get; set; }
}
