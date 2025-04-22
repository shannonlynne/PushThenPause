namespace PushThenPause.Data.Models;

public class UserTask
{
    public int UserTaskId { get; set; }
    public int UserId { get; set; }

    public string Title { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }
    public string? Notes { get; set; }
    public bool IsRecurring { get; set; }
}
