using BHS.API.Application.Commands.NotifyCommand;
using BHS.API.Services;
using BHS.Domain.Entities.Notify;
using BHS.Domain.Entities.Users;
using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BHS.API.Application.Handlers.NotifyHandler;

public class CreateNotificationSetUpHandler : IRequestHandler<CreateNotificationSetUp, bool>
{
    private readonly IBackgroundJob _backgroundJob;
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateNotificationSetUpHandler(IMapper mapper, IIdentityService identityService, IUnitOfWork unitOfWork,
        IBackgroundJob backgroundJob)
    {
        _mapper = mapper;
        _identityService = identityService;
        _unitOfWork = unitOfWork;
        _backgroundJob = backgroundJob;
    }

    public async Task<bool> Handle(CreateNotificationSetUp request, CancellationToken cancellationToken)
    {
        var dateTimeStart = request.TimeStart.UtcDateTime;
        var notificationSetUp = _mapper.Map<CreateNotificationSetUp, NotificationSetUp>(request);
        notificationSetUp.DatetimeStart = dateTimeStart;
        notificationSetUp.Status = CommonStatus.Active;
        notificationSetUp.NotifyMessages = new List<NotifyMessage>();
        var listUser = await _unitOfWork.Repository<User>().Get().Include(x => x.UserFollowVendors)
            .Include(x => x.UserSettings).Where(x => x.UserSettings!.IsGetNotifications &&
                                                     x.UserFollowVendors!.Any(f =>
                                                         !request.VendorId.HasValue ||
                                                         (f.IsFollow && f.VendorId == request.VendorId)))
            .ToListAsync(cancellationToken);
        /* Nếu ToCurrentUser = true thì chỉ gửi thông báo cho User hiện tại */
        if (request.ToCurrentUser) listUser = listUser.Where(x => x.Id == _identityService.GetUserIdentity()).ToList();

        if (!listUser.Any())
            return false;
        foreach (var item in listUser)
            notificationSetUp.NotifyMessages.Add(new NotifyMessage
            {
                NotificationSetUp = notificationSetUp,
                UserId = item.Id,
                Seen = false,
                IsShow = false,
                FcmMessage = ""
            });
        await _unitOfWork.Repository<NotificationSetUp>().InsertAsync(notificationSetUp);
        var result = await _unitOfWork.SaveChangesAsync(cancellationToken);
        /* Tạo 1 background để đến DatetimeStart cập nhật lại IsShow = true */
        _backgroundJob.ScheduleUpdateNotifiesStatusAsync(notificationSetUp.Id, request.TimeStart);
        return result;
    }
}