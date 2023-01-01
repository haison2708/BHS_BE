using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities.Users;

public class UserAppToken : Entity<int>, IAggregateRoot
{
    public string? UserId { get; set; }
    public string? AppId { get; set; }
    public string? Token { get; set; }
    public int Environment { get; set; }
    public User? User { get; set; }
}