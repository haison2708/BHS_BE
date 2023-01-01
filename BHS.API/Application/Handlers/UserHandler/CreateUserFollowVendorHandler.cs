using BHS.API.Application.Commands.UserCommand;
using BHS.API.Services;
using BHS.Domain.Entities.Users;
using BHS.Domain.SeedWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BHS.API.Application.Handlers.UserHandler;

public class CreateUserFollowVendorHandler : IRequestHandler<CreateUserFollowVendor, bool>
{
    private readonly IIdentityService _identityService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserFollowVendorHandler(IUnitOfWork unitOfWork, IIdentityService identityService)
    {
        _unitOfWork = unitOfWork;
        _identityService = identityService;
    }

    public async Task<bool> Handle(CreateUserFollowVendor request, CancellationToken cancellationToken)
    {
        var listUserFollowVendor = await _unitOfWork.Repository<UserFollowVendor>().Get().Where(x =>
            x.UserId == _identityService.GetUserIdentity() &&
            request.VendorIds!.Contains(x.VendorId.ToString())).ToListAsync(cancellationToken);
        foreach (var item in listUserFollowVendor) item.IsFollow = request.IsFollow;
        /* Lấy những vendorId chưa có trong UserFollowVendor và thêm vào */
        IList<UserFollowVendor> listUser = (from item in request.VendorIds!.Split(",")
            where listUserFollowVendor.All(x => x.VendorId != int.Parse(item))
            select new UserFollowVendor
                { VendorId = int.Parse(item), UserId = _identityService.GetUserIdentity(), IsFollow = true }).ToList();
        await _unitOfWork.Repository<UserFollowVendor>().InsertRangeAsync(listUser.AsEnumerable());
        return await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}