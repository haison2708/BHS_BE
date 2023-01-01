using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities.Products;

public class ProductImage : Entity<int>, IAggregateRoot
{
    public string? Url { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
}