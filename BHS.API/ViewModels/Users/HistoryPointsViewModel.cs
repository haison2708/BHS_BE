namespace BHS.API.ViewModels.Users;

public class HistoryPointsViewModel
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? ProgramName { get; set; }
    public int Point { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public DateTime? CreatedAt { get; set; }
}