using System.Text.Json.Serialization;
using BHS.API.ViewModels.LoyaltyPrograms;
using BHS.API.ViewModels.Products;
using BHS.API.ViewModels.Users;

namespace BHS.API.ViewModels.Vendor;

public class VendorViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? ContactName { get; set; }
    public string? ContactPhone { get; set; }
    public string? ContactEmail { get; set; }
    public string? Website { get; set; }
    public string? Image { get; set; }
    public string? Logo { get; set; }
    public string? Info { get; set; }
    public string? TaxCode { get; set; }
    public string? UserId { get; set; }
    public string? ShortName { get; set; }
    public string? Code { get; set; }
    public int TotalGift { get; set; }
    public int TotalPoint { get; set; }
    public int LuckyWheelTurns { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public IList<LoyaltyProgramViewModel>? LoyaltyProgram { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public IList<ProductViewModel>? Products { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IList<ConfigRankOfVendorViewModel>? ConfigRankOfVendor { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public PointOfUserViewModel? AboutToExpire { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public RankOfUserViewModel? RankOfUser { get; set; }
}