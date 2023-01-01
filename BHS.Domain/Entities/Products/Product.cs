using BHS.Domain.Entities.LoyaltyPrograms;
using BHS.Domain.Entities.Users;
using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities.Products;

public class Product : Entity<int>, IAggregateRoot
{
    public string? ProductCode { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public string? Unit { get; set; }
    public string? Descr { get; set; }
    public string? ImgBannerUrl { get; set; }
    public CommonStatus Status { get; set; }
    public bool IsDeleted { get; set; } = false;
    public int ParentProductId { get; set; }
    public int InventoryId { get; set; }
    public bool IsShow { get; set; } = false;
    public float UnitRate { get; set; }
    public float Qty { get; set; }
    public bool IsPromotion { get; set; } = false;
    public string? PromotionTag { get; set; }
    public byte[]? Tstamp { get; set; }
    public string? Barcode { get; set; }

    public Cart? Cart { get; set; }
    public IList<ProductImage>? ProductImages { get; set; }
    public IList<PromotionalProduct>? PromotionalProduct { get; set; }
    public IList<ProductForUser>? ProductForUsers { get; set; }
    public ParentProduct? ParentProduct { get; set; }
    public IList<AttributeValue>? AttributeValues { get; set; }
    public IList<ProductParticipatingLoyalty>? ProductParticipatingLoyalties { get; set; }
}