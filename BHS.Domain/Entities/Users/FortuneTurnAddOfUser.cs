using BHS.Domain.Entities.Fortunes;
using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities.Users;

public class FortuneTurnAddOfUser : Entity<int>, IAggregateRoot
{
    public string? UserId { get; set; }
    public int FortuneId { get; set; }

    public int TurnAdd { get; set; }

    /*public string Code { get; set; }
    public string RewardType { get; set; }*/
    public User? User { get; set; }
    public Fortune? Fortune { get; set; }
}