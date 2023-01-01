using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities.Products;

public class PromotionalProduct : Entity<int>, IAggregateRoot
{
    public int VendorId { get; set; }
    public int ParentProductId { get; set; }
    public int ProductId { get; set; }
    public int PercentPromo { get; set; }
    public decimal AmountPromo { get; set; }
    public CommonStatus Status { get; set; }
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
    public Product? Product { get; set; }
}