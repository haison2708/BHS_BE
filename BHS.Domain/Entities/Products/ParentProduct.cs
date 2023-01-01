using BHS.Domain.Entities.Categories;
using BHS.Domain.Entities.Vendors;
using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities.Products;

public class ParentProduct : Entity<int>, IAggregateRoot
{
    public string? Name { get; set; }
    public string? BrandName { get; set; }
    public string? Descr { get; set; }
    public string? ImgBanner { get; set; }
    public string? VideoUrl { get; set; }
    public CommonStatus Status { get; set; }
    public int CategoryId { get; set; }
    public int ParentCategoryId { get; set; }
    public string? StkUnit { get; set; }
    public int? Highlight { get; set; }
    public string? Barcode { get; set; }
    public string? GroupCode { get; set; }
    public string? ProductCode { get; set; }
    public int VendorId { get; set; }

    public Category? Category { get; set; }
    public Vendor? Vendor { get; set; }

    public IList<Product>? Products { get; set; }
}