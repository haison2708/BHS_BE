using System.Text.Json.Serialization;
using BHS.API.ViewModels.Fortunes;

namespace BHS.API.ViewModels.Users;

public class FortuneTurnAddOfUserViewModel
{
    public string? UserId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public FortuneViewModel? Fortune { get; set; }

    public int TurnAdd { get; set; }
    public string? Code { get; set; }
    public string? RewardType { get; set; }
}