namespace PushThenPause.Data.Models;

public class StreakTracker
{
    public int StreakTrackerId { get; set; }
    public int UserId { get; set; }

    public DateTime Date { get; set; }
    public int StepsCompleted { get; set; }
    public int BreaksTaken { get; set; }

    public User User { get; set; } = null!;
}
