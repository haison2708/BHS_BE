using BHS.Domain.Entities.Vendors;
using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities.Users;

public class UserFollowVendor : Entity<int>, IAggregateRoot
{
    public string? UserId { get; set; }
    public int VendorId { get; set; }
    public bool IsFollow { get; set; }
    public User? User { get; set; }
    public Vendor? Vendor { get; set; }
}