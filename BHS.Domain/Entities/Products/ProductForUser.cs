using BHS.Domain.Entities.Users;
using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities.Products;

public class ProductForUser : Entity<int>, IAggregateRoot
{
    public int ProductId { get; set; }
    public string? UserId { get; set; }
    public ProductForUserType Type { get; set; }
    public Product? Product { get; set; }
    public User? User { get; set; }
}