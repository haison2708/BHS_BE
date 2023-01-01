using BHS.API.ViewModels.Fortunes;
using BHS.Domain.Enumerate;
using MediatR;

namespace BHS.API.Application.Commands.FortuneCommand;

public class CreateFortuneDetail : IRequest<FortuneDetailViewModel>
{
    public int FortuneId { get; set; }
    public string? Descr { get; set; }
    public int Probability { get; set; }
    public int Limit { get; set; }
    public FortuneType FortuneType { get; set; }
    public int Quantity { get; set; }
}