namespace PushThenPause.Data.Models;

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    public ICollection<UserTask> Tasks { get; set; } = new List<UserTask>();
    public ICollection<BreakActivity> BreakActivities { get; set; } = new List<BreakActivity>();
    public ICollection<Cycle> Cycles { get; set; } = new List<Cycle>();
    public StreakTracker? StreakTracker { get; set; }
    public NemsModeSettings? NemsModeSettings { get; set; }
}
