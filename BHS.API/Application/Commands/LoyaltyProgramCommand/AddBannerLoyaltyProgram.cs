using BHS.API.ViewModels.LoyaltyPrograms;
using MediatR;

namespace BHS.API.Application.Commands.LoyaltyProgramCommand;

public class AddBannerLoyaltyProgram : IRequest<LoyaltyProgramViewModel>
{
    public int LoyaltyProgramId { get; set; }
    public IFormFile? ImageBanner { get; set; }
}