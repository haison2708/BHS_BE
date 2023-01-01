using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities.Chat;

public class SeenMessage : Entity<int>
{
    public int MessageId { get; set; }
    public int ParticipantId { get; set; }
    public DateTime SeenTime { get; set; }
    public Message? Message { get; set; }
    public Participant? Participant { get; set; }
}