using BHS.API.ViewModels.Products;
using BHS.API.ViewModels.Vendor;

namespace BHS.API.ViewModels.Cart;

public class CartViewModel
{
    public int CartId { get; set; }
    public string? UserId { get; set; }
    public ParentProductViewModel? ParentProduct { get; set; }
    public VendorViewModel? Vendor { get; set; }
    public int Quantity { get; set; }
}