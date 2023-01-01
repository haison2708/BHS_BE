using BHS.Domain.Entities.Notify;
using BHS.Domain.Entities.Products;
using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities.Users;

public class User : Entity<string>, IAggregateRoot
{
    public string? DisplayName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public DateTime Birthday { get; set; }
    public bool Gender { get; set; }
    public string? Avatar { get; set; }
    public bool Status { get; set; }
    public IList<Cart>? Carts { get; set; }
    public IList<NotifyMessage>? NotifyMessages { get; set; }
    public IList<PointOfUser>? PointOfUsers { get; set; }
    public IList<UserFollowVendor>? UserFollowVendors { get; set; }
    public IList<ProductForUser>? ProductForUsers { get; set; }
    public IList<GiftOfUser>? GiftOfUsers { get; set; }
    public IList<UserAppToken>? UserAppToken { get; set; }
    public UserSettings? UserSettings { get; set; }
}