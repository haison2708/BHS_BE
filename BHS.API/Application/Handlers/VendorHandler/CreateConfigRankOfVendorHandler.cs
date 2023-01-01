using BHS.API.Application.Commands.VendorCommand;
using BHS.API.ViewModels.Vendor;
using BHS.Domain.Entities.Vendors;
using BHS.Domain.SeedWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BHS.API.Application.Handlers.VendorHandler;

public class CreateConfigRankOfVendorHandler : IRequestHandler<CreateConfigRankOfVendor, object>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateConfigRankOfVendorHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(CreateConfigRankOfVendor request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Repository<ConfigRankOfVendor>().Get()
            .FirstOrDefaultAsync(x => x.VendorId == request.VendorId, cancellationToken);
        if (result is null)
        {
            var settings = new ConfigRankOfVendor
            {
                VendorId = request.VendorId,
                Name = request.Name,
                Points = request.Points
            };
            result = await _unitOfWork.Repository<ConfigRankOfVendor>().InsertAsync(settings);
        }
        else
        {
            result.VendorId = request.VendorId;
            result.Name = request.Name;
            result.Points = request.Points;
        }

        return (await _unitOfWork.SaveChangesAsync(cancellationToken)
            ? new ConfigRankOfVendorViewModel
            {
                VendorId = result.VendorId,
                Name = request.Name,
                Points = request.Points
            }
            : null)!;
    }
}