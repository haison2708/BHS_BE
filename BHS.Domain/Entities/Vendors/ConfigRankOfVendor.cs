using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities.Vendors;

public class ConfigRankOfVendor : Entity<int>, IAggregateRoot
{
    public int VendorId { get; set; }
    public string? Name { get; set; }
    public int Points { get; set; }
    public Vendor? Vendor { get; set; }
}