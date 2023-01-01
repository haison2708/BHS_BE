using BHS.API.Application.Commands.NotifyCommand;
using BHS.API.Services;
using BHS.Domain.Entities.Notify;
using BHS.Domain.SeedWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BHS.API.Application.Handlers.NotifyHandler;

public class UpdateNotifyHandler : IRequestHandler<UpdateNotify>
{
    private readonly IIdentityService _identityService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateNotifyHandler(IUnitOfWork unitOfWork, IIdentityService identityService)
    {
        _unitOfWork = unitOfWork;
        _identityService = identityService;
    }

    public async Task<Unit> Handle(UpdateNotify request, CancellationToken cancellationToken)
    {
        /* Nếu notifyId != null cập nhật thông báo có Id = notifyId thành đã xem, cập nhật tất cả thông báo chưa xem của User hiện tại thành đã xem */
        if (request.NotifyId is not null)
        {
            var notify = await _unitOfWork.Repository<NotifyMessage>().Get()
                .FirstOrDefaultAsync(x => x.Id == request.NotifyId, cancellationToken);
            if (notify is null)
                return await Task.FromResult(Unit.Value);
            notify.Seen = true;
            notify.SeenTime = DateTime.UtcNow;
        }
        else
        {
            var notifies = await _unitOfWork.Repository<NotifyMessage>().Get().Where(x =>
                x.UserId == _identityService.GetUserIdentity()
                && x.IsShow == true && x.Seen == false).ToListAsync(cancellationToken);
            foreach (var item in notifies)
            {
                item.Seen = true;
                item.SeenTime = DateTime.UtcNow;
            }
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return await Task.FromResult(Unit.Value);
    }
}