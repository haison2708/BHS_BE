using BHS.Domain.Entities.Users;
using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities.LoyaltyPrograms;

public class GiftOfLoyalty : Entity<int>, IAggregateRoot
{
    public string? Name { get; set; }
    public int SourceId { get; set; }
    public int LoyaltyProgramId { get; set; }
    public int Quantity { get; set; }
    public int Point { get; set; }
    public int Limit { get; set; }
    public int QtyAvailable { get; set; }
    public GiftType Type { get; set; }
    public int FromDateOfExchange { get; set; }
    public LoyaltyProgram? LoyaltyProgram { get; set; }
    public IList<GiftOfUser>? GiftOfUsers { get; set; }
}