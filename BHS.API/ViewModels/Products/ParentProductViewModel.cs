using System.Text.Json.Serialization;
using BHS.API.ViewModels.Vendor;

namespace BHS.API.ViewModels.Products;

public class ParentProductViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? BrandName { get; set; }
    public string? Descr { get; set; }
    public string? ImgBanner { get; set; }
    public string? VideoUrl { get; set; }
    public int? Status { get; set; }
    public int CategoryId { get; set; }
    public int ParentCategoryId { get; set; }
    public string? StkUnit { get; set; }
    public int? Highlight { get; set; }
    public string? Barcode { get; set; }
    public string? GroupCode { get; set; }
    public string? ProductCode { get; set; }
    public int VendorId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public VendorViewModel? Vendor { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<ProductViewModel>? Products { get; set; }
}