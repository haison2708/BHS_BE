namespace BHS.API.ViewModels.Products;

public class PromotionalProductViewModel
{
    public int Id { get; set; }
    public int VendorId { get; set; }
    public int ParentProductId { get; set; }
    public int ProductId { get; set; }
    public int PercentPromo { get; set; }
    public decimal AmountPromo { get; set; }
    public int Status { get; set; }
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
}