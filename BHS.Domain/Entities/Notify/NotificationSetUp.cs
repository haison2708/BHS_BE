using BHS.Domain.Entities.Vendors;
using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities.Notify;

public class NotificationSetUp : Entity<int>, IAggregateRoot
{
    public string? Title { get; set; }
    public string? SubTitle { get; set; }
    public int Type { get; set; }
    public DateTime DatetimeStart { get; set; }
    public string? Remark { get; set; }
    public string? Content { get; set; }
    public int? VendorId { get; set; }
    public CommonStatus Status { get; set; }
    public string? AttachFile { get; set; }
    public IList<NotifyMessage>? NotifyMessages { get; set; }
    public Vendor? Vendor { get; set; }
}