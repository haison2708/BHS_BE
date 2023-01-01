using System.Text.Json.Serialization;
using BHS.API.ViewModels.Vendor;

namespace BHS.API.ViewModels.Fortunes;

public class FortuneViewModel
{
    public int Id { get; set; }
    public string? Descr { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public string? ImageBanner { get; set; }
    public int TurnsOfUser { get; set; }
    public VendorViewModel? Vendor { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IList<FortuneDetailViewModel>? FortuneDetails { get; set; }
}