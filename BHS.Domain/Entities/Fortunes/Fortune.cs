using BHS.Domain.Entities.Users;
using BHS.Domain.Entities.Vendors;
using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities.Fortunes;

public class Fortune : Entity<int>, IAggregateRoot
{
    public int VendorId { get; set; }
    public string? Descr { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public string? ImageBanner { get; set; }
    public CommonStatus Status { get; set; }
    public Vendor? Vendor { get; set; }
    public IList<FortuneDetail>? FortuneDetails { get; set; }
    public IList<FortuneTurnOfUser>? FortuneTurnOfUsers { get; set; }
    public IList<FortuneTurnAddOfUser>? FortuneTurnAddOfUsers { get; set; }
    public IList<FortuneUserReward>? FortuneUserRewards { get; set; }
}