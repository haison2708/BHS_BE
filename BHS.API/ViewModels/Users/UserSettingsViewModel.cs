namespace BHS.API.ViewModels.Users;

public class UserSettingsViewModel
{
    public bool IsFingerprintLogin { get; set; }
    public bool IsGetNotifications { get; set; }
    public string? LangId { get; set; }
    public int VendorId { get; set; }
}