using BHS.API.ViewModels.Fortunes;
using MediatR;

namespace BHS.API.Application.Commands.FortuneCommand;

public class AddFortuneDetailImage : IRequest<FortuneDetailViewModel>
{
    public int FortuneDetailId { get; set; }
    public IFormFile? Image { get; set; }
}