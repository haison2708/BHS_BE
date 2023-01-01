using BHS.API.ViewModels.Fortunes;
using MediatR;

namespace BHS.API.Application.Commands.FortuneCommand;

public class AddBannerFortune : IRequest<FortuneViewModel>
{
    public int FortuneId { get; set; }
    public IFormFile? ImageBanner { get; set; }
}