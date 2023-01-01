using System.Text.Json.Serialization;
using BHS.API.ViewModels.LoyaltyPrograms;

namespace BHS.API.ViewModels.Users;

public class GiftOfUserViewModel
{
    public int Id { get; set; }
    public bool IsUsed { get; set; }
    public DateTime ExpirationDate { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public GiftOfLoyaltyViewModel? GiftOfLoyalty { get; set; }
}