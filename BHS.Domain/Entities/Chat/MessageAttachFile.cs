using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities.Chat;

public class MessageAttachFile : Entity<int>
{
    public int MessageId { get; set; }
    public string? Url { get; set; }
    public FileType Type { get; set; }
}