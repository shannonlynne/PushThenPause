namespace PushThenPause.Business.Dtos;

public class CycleDto
{
    public string BreakActivityTitle { get; set; } = string.Empty;
    public string UserTaskTitle { get; set; } = string.Empty;
    public int BreakActivityDuration { get; set; }
    public int UserTaskDuration { get; set; }
}