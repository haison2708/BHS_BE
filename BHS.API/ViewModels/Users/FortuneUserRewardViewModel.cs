using System.Text.Json.Serialization;
using BHS.API.ViewModels.Fortunes;

namespace BHS.API.ViewModels.Users;

public class FortuneUserRewardViewModel
{
    public int Id { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public FortuneViewModel? Fortune { get; set; }

    public DateTime CreatedAt { get; set; }
}