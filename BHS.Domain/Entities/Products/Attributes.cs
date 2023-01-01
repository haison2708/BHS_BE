using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities.Products;

public class Attributes : Entity<int>, IAggregateRoot
{
    public string? Name { get; set; }
    public IList<AttributeValue>? AttributeValues { get; set; }
}