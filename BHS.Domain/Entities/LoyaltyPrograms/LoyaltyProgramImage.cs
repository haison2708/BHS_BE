using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities.LoyaltyPrograms;

public class LoyaltyProgramImage : Entity<int>, IAggregateRoot
{
    public int LoyaltyProgramId { get; set; }
    public string? ImageUrl { get; set; }
    public LoyaltyProgram? LoyaltyProgram { get; set; }
}