namespace BHS.API.ViewModels.Notify;

public class NotificationSetupViewModel
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? SubTitle { get; set; }
    public int Type { get; set; }
    public DateTime DatetimeStart { get; set; }
    public string? Remark { get; set; }
    public string? Content { get; set; }
    public int? VendorId { get; set; }
    public string? AttachFile { get; set; }
}