using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities.Products;

public class AttributeValue : Entity<int>, IAggregateRoot
{
    public int ProductId { get; set; }
    public int AttributesId { get; set; }
    public string? Value { get; set; }
    public Product? Product { get; set; }
    public Attributes? Attributes { get; set; }
}