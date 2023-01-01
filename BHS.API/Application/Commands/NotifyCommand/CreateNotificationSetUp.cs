using BHS.Domain.Enumerate;
using MediatR;

namespace BHS.API.Application.Commands.NotifyCommand;

public class CreateNotificationSetUp : IRequest<bool>
{
    public string? Title { get; set; }
    public string? SubTitle { get; set; }
    public NotifyType Type { get; set; }
    public DateTimeOffset TimeStart { get; set; }
    public string? Content { get; set; }
    public string? Remark { get; set; }
    public int? VendorId { get; set; }
    public bool ToCurrentUser { get; set; }
}