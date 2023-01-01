using System.Text.Json.Serialization;
using BHS.API.ViewModels.Fortunes;
using BHS.API.ViewModels.Products;

namespace BHS.API.ViewModels.LoyaltyPrograms;

public class GiftOfLoyaltyViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ProductViewModel? Product { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public FortuneViewModel? Fortune { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public LoyaltyProgramViewModel? LoyaltyProgram { get; set; }

    public int Quantity { get; set; }
    public int Point { get; set; }
    public int Limit { get; set; }
    public int QtyAvailable { get; set; }
}