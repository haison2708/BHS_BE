namespace BHS.API.ViewModels.Fortunes;

public class FortuneDetailViewModel
{
    public int Id { get; set; }
    public int FortuneId { get; set; }
    public string? Descr { get; set; }
    public string? Image { get; set; }
    public int Limit { get; set; }
    public int QtyAvailable { get; set; }
    public int FortuneType { get; set; }
    public int Quantity { get; set; }
}