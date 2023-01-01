using BHS.Domain.Entities.Products;
using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities.LoyaltyPrograms;

public class ProductParticipatingLoyalty : Entity<int>, IAggregateRoot
{
    public int ProductId { get; set; }
    public int LoyaltyProgramId { get; set; }
    public CommonStatus Status { get; set; }
    public int Points { get; set; }
    public int AmountOfMoney { get; set; }
    public LoyaltyProgramType Type { get; set; }
    public Product? Product { get; set; }
    public LoyaltyProgram? LoyaltyProgram { get; set; }
    public IList<BarCodeOfProductParticipatingLoyalty>? BarCodeOfProductParticipatingLoyalty { get; set; }
}