using BHS.Domain.Entities.Users;
using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities.Chat;

public class Participant : Entity<int>
{
    public string? UserId { get; set; }
    public int ConversationId { get; set; }
    public ConversationRole Role { get; set; }
    public bool SharedAll { get; set; }
    public bool IsRemove { get; set; }
    public bool IsDeletedConversation { get; set; }
    public DateTime DeletedConversationAt { get; set; }
    public bool IsCurrentSupporter { get; set; }
    public Conversation? Conversation { get; set; }
    public User? User { get; set; }
}