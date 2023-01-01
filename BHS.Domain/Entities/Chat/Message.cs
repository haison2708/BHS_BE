using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities.Chat;

public class Message : Entity<int>
{
    public int ParticipantId { get; set; }
    public string? Content { get; set; }
    public ContentType ContentType { get; set; }
    public bool IsDeleted { get; set; }
    public Participant? Participant { get; set; }
}