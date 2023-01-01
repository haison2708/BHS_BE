using BHS.API.ViewModels.Fortunes;
using MediatR;

namespace BHS.API.Application.Commands.FortuneCommand;

public class CreateFortune : IRequest<FortuneViewModel>
{
    public int VendorId { get; set; }
    public string? Descr { get; set; }
    public DateTimeOffset FromDate { get; set; }
    public DateTimeOffset ToDate { get; set; }
}