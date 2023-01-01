using System.Text.Json.Serialization;
using BHS.API.ViewModels.Fortunes;

namespace BHS.API.ViewModels.Users;

public class FortuneTurnOfUserViewModel
{
    public int Id { get; set; }
    public string? UserId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public FortuneViewModel? Fortune { get; set; }

    public int TurnTotal { get; set; }
    public int TurnAvailable { get; set; }
}