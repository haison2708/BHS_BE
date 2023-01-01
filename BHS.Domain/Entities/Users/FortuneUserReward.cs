using BHS.Domain.Entities.Fortunes;
using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities.Users;

public class FortuneUserReward : Entity<int>, IAggregateRoot
{
    public string? UserId { get; set; }
    public int FortuneId { get; set; }
    public int FortuneDetailId { get; set; }
    public User? User { get; set; }
    public Fortune? Fortune { get; set; }
    public FortuneDetail? FortuneDetail { get; set; }
}