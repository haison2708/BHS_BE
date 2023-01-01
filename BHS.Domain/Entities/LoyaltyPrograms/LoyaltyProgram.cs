using BHS.Domain.Entities.Vendors;
using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities.LoyaltyPrograms;

public class LoyaltyProgram : Entity<int>, IAggregateRoot
{
    public int VendorId { get; set; }
    public string? Name { get; set; }
    public string? ImgBannerUrl { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public CommonStatus Status { get; set; }
    public LoyaltyProgramType Type { get; set; }
    public Vendor? Vendor { get; set; }
    public IList<LoyaltyProgramImage>? LoyaltyProgramImages { get; set; }
    public IList<ProductParticipatingLoyalty>? ProductParticipatingLoyalty { get; set; }
    public IList<GiftOfLoyalty>? GiftOfLoyalty { get; set; }
}