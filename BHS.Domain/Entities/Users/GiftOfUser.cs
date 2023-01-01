using BHS.Domain.Entities.LoyaltyPrograms;
using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities.Users;

public class GiftOfUser : Entity<int>, IAggregateRoot
{
    public string? UserId { get; set; }
    public int VendorId { get; set; }
    public int GiftOfLoyaltyId { get; set; }
    public bool IsUsed { get; set; }
    public DateTime ExpirationDate { get; set; }
    public User? User { get; set; }
    public GiftOfLoyalty? GiftOfLoyalty { get; set; }
}