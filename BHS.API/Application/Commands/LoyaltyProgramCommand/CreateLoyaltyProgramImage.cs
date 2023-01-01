using BHS.API.ViewModels.LoyaltyPrograms;
using MediatR;

namespace BHS.API.Application.Commands.LoyaltyProgramCommand;

public class CreateLoyaltyProgramImage : IRequest<LoyaltyProgramViewModel>
{
    public int LoyaltyProgramId { get; set; }
    public IList<IFormFile>? LoyaltyProgramImages { get; set; }
}