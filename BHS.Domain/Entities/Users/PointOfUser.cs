using BHS.Domain.Entities.Vendors;
using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities.Users;

public class PointOfUser : Entity<int>, IAggregateRoot
{
    public int VendorId { get; set; }
    public string? UserId { get; set; }
    public int Point { get; set; }
    public PointOfUserType Type { get; set; }
    public PointOfUserType ProgramType { get; set; }
    public int SourceId { get; set; }
    public int SourceDetailId { get; set; }
    //public DateTime? ExpirationDate { get; set; }
    public User? User { get; set; }
    public Vendor? Vendor { get; set; }
}