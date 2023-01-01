using System.Text.Json.Serialization;
using BHS.API.ViewModels.Products;

namespace BHS.API.ViewModels.LoyaltyPrograms;

public class ProductParticipatingLoyaltyViewModel
{
    public int Id { get; set; }
    public int Points { get; set; }
    public int AmountOfMoney { get; set; }
    public int Type { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ProductViewModel? Product { get; set; }
}