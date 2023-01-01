using BHS.Domain.Entities.Vendors;
using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities.Categories;

public class CategoryOfVendor : Entity<int>, IAggregateRoot
{
    public int VendorId { get; set; }
    public int CategoryId { get; set; }
    public CommonStatus Status { get; set; }
    public Category? Category { get; set; }
    public Vendor? Vendor { get; set; }
}