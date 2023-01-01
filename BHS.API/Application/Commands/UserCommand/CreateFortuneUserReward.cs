using BHS.API.ViewModels.Fortunes;
using MediatR;

namespace BHS.API.Application.Commands.UserCommand;

public class CreateFortuneUserReward : IRequest<FortuneDetailViewModel>
{
    public int FortuneId { get; set; }
}