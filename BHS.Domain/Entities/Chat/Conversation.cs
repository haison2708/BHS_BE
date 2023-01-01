using BHS.Domain.Entities.Users;
using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities.Chat;

public class Conversation : Entity<int>
{
    public string? UserId { get; set; }
    public string? Name { get; set; }
    public ConversationType Type { get; set; }
    public User? User { get; set; }
    public IList<Participant>? Participants { get; set; }
}