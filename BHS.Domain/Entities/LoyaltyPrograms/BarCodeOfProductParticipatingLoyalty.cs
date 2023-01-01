using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities.LoyaltyPrograms;

public class BarCodeOfProductParticipatingLoyalty : Entity<int>, IAggregateRoot
{
    public string? BarCode { get; set; }
    public int ProductParticipatingId { get; set; }
    public bool IsUsed { get; set; }
    public ProductParticipatingLoyalty? ProductParticipating { get; set; }
}