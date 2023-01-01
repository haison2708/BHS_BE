using System.Text.Json.Serialization;

namespace BHS.API.ViewModels.Products;

public class ProductViewModel
{
    public int Id { get; set; }
    public string? ProductCode { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public decimal PricePromotion { get; set; }
    public string? Unit { get; set; }
    public string? Descr { get; set; }
    public string? ImgBannerUrl { get; set; }
    public int ParentProductId { get; set; }
    public float UnitRate { get; set; }
    public float Qty { get; set; }
    public bool IsPromotion { get; set; }
    public string? PromotionTag { get; set; }
    public byte[]? Tstamp { get; set; }

    public string? Barcode { get; set; }

    /*public int CategoryId { get; set; }
    public int ParentCategoryId { get; set; }
    public int InventoryId { get; set; }
    public bool IsShow { get; set; }
    public int Status { get; set; }
    public bool IsDeleted { get; set; }*/
    //public PromotionalProductViewModel? PromotionalProduct { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IList<AttributeValueViewModel>? AttributeValues { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<ProductImagesViewModel>? ProductImages { get; set; }
}