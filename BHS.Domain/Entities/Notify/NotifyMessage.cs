using BHS.Domain.Entities.Users;
using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities.Notify;

public class NotifyMessage : Entity<int>, IAggregateRoot
{
    public int NotificationSetUpId { get; set; }
    public string? UserId { get; set; }
    public DateTime? SeenTime { get; set; }
    public bool Seen { get; set; }
    public bool IsShow { get; set; }
    public string? FcmMessage { get; set; }
    public NotificationSetUp? NotificationSetUp { get; set; }
    public User? User { get; set; }
}