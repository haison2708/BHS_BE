using BHS.Domain.Entities.Fortunes;
using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities.Users;

public class FortuneTurnOfUser : Entity<int>, IAggregateRoot
{
    public string? UserId { get; set; }
    public int FortuneId { get; set; }
    public int TurnTotal { get; set; }
    public int TurnAvailable { get; set; }
    public User? User { get; set; }
    public Fortune? Fortune { get; set; }
}