namespace BHS.API.ViewModels.Users;

public class TotalPointOfProgramViewModel
{
    public int ProgramId { get; set; }
    public string? ProgramName { get; set; }
    public int TotalPoints { get; set; }
    public DateTime? ExpirationDate { get; set; }
}