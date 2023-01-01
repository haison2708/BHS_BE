using BHS.Domain.Entities.Languages;
using BHS.Domain.Entities.Vendors;
using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities.Users;

public class UserSettings : Entity, IAggregateRoot
{
    public string? UserId { get; set; }
    public bool IsFingerprintLogin { get; set; }
    public bool IsGetNotifications { get; set; }
    public string? LangId { get; set; }
    public int? VendorId { get; set; }
    public User? User { get; set; }
    public Language? Language { get; set; }
    public Vendor? Vendor { get; set; }
}