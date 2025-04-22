namespace PushThenPause.Data.Models;

public class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public DateOnly DateCreated { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    public string Email { get; set; } = string.Empty;
}
