using BHS.API.Application.Commands.UserCommand;
using BHS.API.Services;
using BHS.API.ViewModels.Users;
using BHS.Domain.Entities.Users;
using BHS.Domain.SeedWork;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BHS.API.Application.Handlers.UserHandler;

public class CreateUserSettingsHandler : IRequestHandler<CreateUserSettings, UserSettingsViewModel>
{
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserSettingsHandler(IUnitOfWork unitOfWork, IMapper mapper, IIdentityService identityService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _identityService = identityService;
    }

    public async Task<UserSettingsViewModel> Handle(CreateUserSettings request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Repository<UserSettings>().Get()
            .FirstOrDefaultAsync(x => x.UserId == _identityService.GetUserIdentity(), cancellationToken);
        /* Nếu User chưa có setting thì thêm vào, ngược lại thì cập nhật */
        if (result is null)
        {
            var newUserSetting = _mapper.Map<UserSettings>(request);
            newUserSetting.UserId = _identityService.GetUserIdentity();
            result = await _unitOfWork.Repository<UserSettings>().InsertAsync(newUserSetting);
        }
        else
        {
            result.IsFingerprintLogin = request.IsFingerprintLogin;
            result.IsGetNotifications = request.IsGetNotifications;
            result.LangId = request.LangId!;
            result.VendorId = request.VendorId;
        }

        return (await _unitOfWork.SaveChangesAsync(cancellationToken)
            ? _mapper.Map<UserSettingsViewModel>(result)
            : null)!;
    }
}