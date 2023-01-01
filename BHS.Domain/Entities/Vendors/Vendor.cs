using BHS.Domain.Entities.Categories;
using BHS.Domain.Entities.Fortunes;
using BHS.Domain.Entities.LoyaltyPrograms;
using BHS.Domain.Entities.Notify;
using BHS.Domain.Entities.Products;
using BHS.Domain.Entities.Users;
using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities.Vendors;

public class Vendor : Entity<int>, IAggregateRoot
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? ContactName { get; set; }
    public string? ContactPhone { get; set; }
    public string? ContactEmail { get; set; }
    public string? Website { get; set; }
    public string? Image { get; set; }
    public string? Logo { get; set; }
    public string? Info { get; set; }
    public int Status { get; set; }
    public bool IsDeleted { get; set; }
    public string? Fax { get; set; }
    public string? TaxCode { get; set; }
    public float Rating { get; set; }
    public string? VendorKey { get; set; }
    public int TotalFeedback { get; set; }
    public string? ShortName { get; set; }
    public IList<LoyaltyProgram>? LoyaltyPrograms { get; set; }
    public IList<CategoryOfVendor>? CategoryOfVendors { get; set; }
    public IList<NotificationSetUp>? NotificationSetUps { get; set; }
    public IList<UserFollowVendor>? UserFollowVendors { get; set; }
    public IList<Fortune>? Fortunes { get; set; }
    public IList<PointOfUser>? PointOfUsers { get; set; }
    public IList<UserSettings>? UserSettings { get; set; }
    public IList<ConfigRankOfVendor>? ConfigRankOfVendors { get; set; }
    public IList<ParentProduct>? ParentProducts { get; set; }
}