namespace PushThenPause.Data.Models;

public class NemsModeSettings
{
    public int NemsModeSettingsId { get; set; }
    public int UserId { get; set; }

    public bool IsEnabled { get; set; } = true;
    public string EncouragementFrequency { get; set; } = "EveryCycle";
}
