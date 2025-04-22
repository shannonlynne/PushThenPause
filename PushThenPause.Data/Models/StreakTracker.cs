namespace PushThenPause.Data.Models;

public class StreakTracker
{
    public int StreakTrackerId { get; set; } = 0;
    public int UserId { get; set; }

    public int StreakCount { get; set; } = 0;
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
}
