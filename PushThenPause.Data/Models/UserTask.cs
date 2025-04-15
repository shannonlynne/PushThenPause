namespace PushThenPause.Data.Models;

public class UserTask
{
    public int UserTaskId { get; set; }  // ✅ This is the primary key
    public int UserId { get; set; }
    public int TaskCategoryId { get; set; }

    public string Title { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }
    public string? Notes { get; set; }
    public bool IsRecurring { get; set; }

    public User User { get; set; } = null!;
    public TaskCategory TaskCategory { get; set; } = null!;
    public ICollection<Cycle> Cycles { get; set; } = new List<Cycle>();
}
