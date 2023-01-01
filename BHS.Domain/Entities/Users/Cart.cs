using BHS.Domain.Entities.Products;
using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities.Users;

public class Cart : Entity<int>, IAggregateRoot
{
    public string? UserId { get; set; }
    public int VendorId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public bool IsRemove { get; set; }
    public bool IsOrder { get; set; }

    public User? User { get; set; }
    public Product? Product { get; set; }
}