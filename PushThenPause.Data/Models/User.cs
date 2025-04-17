namespace PushThenPause.Data.Models;

public class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = string.Empty;
    public string? DisplayName { get; set; }
    public string Email { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
}
