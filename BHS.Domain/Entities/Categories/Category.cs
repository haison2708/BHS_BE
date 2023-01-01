using BHS.Domain.Entities.Products;
using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities.Categories;

public class Category : Entity<int>, IAggregateRoot
{
    public string? CategoryCode { get; set; }
    public string? Name { get; set; }
    public string? ImageUrl { get; set; }
    public CommonStatus Status { get; set; }
    public bool IsDeleted { get; set; }
    public int ParentId { get; set; }
    public int Sort { get; set; }
    public int Level { get; set; }
    public IList<CategoryOfVendor>? CategoryOfVendors { get; set; }
    public IList<ParentProduct>? ParentProducts { get; set; }
}