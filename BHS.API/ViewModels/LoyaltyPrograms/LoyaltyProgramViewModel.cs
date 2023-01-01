using System.Text.Json.Serialization;
using BHS.API.ViewModels.Vendor;

namespace BHS.API.ViewModels.LoyaltyPrograms;

public class LoyaltyProgramViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? ImgBannerUrl { get; set; }
    public int PointOfUser { get; set; }
    public int Type { get; set; }
    public string? TypeName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime ExpirationDate { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public VendorViewModel? Vendor { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IList<ProductParticipatingLoyaltyViewModel>? ProductParticipatingLoyalties { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IList<LoyaltyProgramImageViewModel>? LoyaltyProgramImages { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IList<GiftOfLoyaltyViewModel>? GiftOfLoyalty { get; set; }
}