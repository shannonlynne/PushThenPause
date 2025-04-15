namespace PushThenPause.Data.Models;

public class TaskCategory
{
    public int TaskCategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? IconUrl { get; set; }
    public string? Color { get; set; }

    public ICollection<UserTask> Tasks { get; set; } = new List<UserTask>();
}
