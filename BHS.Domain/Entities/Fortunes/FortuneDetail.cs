using BHS.Domain.Entities.Users;
using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities.Fortunes;

public class FortuneDetail : Entity<int>, IAggregateRoot
{
    public string? Descr { get; set; }
    public int FortuneId { get; set; }
    public string? Image { get; set; }
    public int Probability { get; set; }
    public int Limit { get; set; }
    public int QtyAvailable { get; set; }
    public FortuneType FortuneType { get; set; }
    public int Quantity { get; set; }
    public Fortune? Fortune { get; set; }
    public IList<FortuneUserReward>? FortuneUserRewards { get; set; }
}