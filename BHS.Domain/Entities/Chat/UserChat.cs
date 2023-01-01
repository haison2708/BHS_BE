using BHS.Domain.Entities.Users;
using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities.Chat;

public class UserChat : Entity
{
    public string? UserId { get; set; }
    public bool IsOnline { get; set; }
    public DateTime LastOnlineTime { get; set; }
    public User? User { get; set; }
}