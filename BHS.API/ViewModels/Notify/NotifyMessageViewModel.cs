using System.Text.Json.Serialization;

namespace BHS.API.ViewModels.Notify;

public class NotifyMessageViewModel
{
    public int Id { get; set; }
    public int NotificationSetupId { get; set; }
    public DateTime? SeenTime { get; set; }
    public bool Seen { get; set; }
    public string? FcmMessage { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public NotificationSetupViewModel? NotificationSetup { get; set; }
}