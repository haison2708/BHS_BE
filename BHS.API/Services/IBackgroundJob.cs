using BHS.API.Application.Commands.NotifyCommand;
using BHS.Domain.Entities.Fortunes;
using BHS.Domain.Entities.LoyaltyPrograms;
using BHS.Domain.Entities.Notify;
using BHS.Domain.Entities.Users;
using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;
using Dapper;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;

namespace BHS.API.Services;

public interface IBackgroundJob : IScopedService
{
    void ScheduleUpdateNotifiesStatusAsync(int notifyId, DateTimeOffset dateTimeOffset);

    void ScheduleUpdatePointAsync(string columnName, int programId, DateTimeOffset dateTimeOffset);
}

public class BackgroundJob : IBackgroundJob
{
    //private readonly IUnitOfWork _unitOfWork;
    private readonly IBackgroundJobClient _backgroundJobClient;
    private readonly IConfiguration _configuration;
    private readonly IHubContext<NotificationHub, INotificationHub> _hubContext;
    private readonly IMediator _mediator;
    private readonly IFcmNotifySender _notifySender;

    public BackgroundJob(IUnitOfWork unitOfWork, IBackgroundJobClient backgroundJobClient,
        IHubContext<NotificationHub, INotificationHub> hubContext, IMediator mediator, IFcmNotifySender notifySender,
        IConfiguration configuration)
    {
        //_unitOfWork = unitOfWork;
        _backgroundJobClient = backgroundJobClient;
        _hubContext = hubContext;
        _mediator = mediator;
        _notifySender = notifySender;
        _configuration = configuration;
    }

    public void ScheduleUpdateNotifiesStatusAsync(int notifyId, DateTimeOffset dateTimeOffset)
    {
        _backgroundJobClient.Schedule(() => UpdateNotifiesStatusAsync(notifyId), dateTimeOffset);
    }

    public void ScheduleUpdatePointAsync(string columnName, int programId, DateTimeOffset dateTimeOffset)
    {
        _backgroundJobClient.Schedule(() => UpdatePointOfUserAsync(columnName, programId), dateTimeOffset);
    }

    public async Task UpdateNotifiesStatusAsync(int notifyId)
    {
        await using var connection = new SqlConnection(_configuration["ConnectionString"]);
        var affectRow =
            await connection.ExecuteAsync(
                $@"UPDATE NotifyMessage SET IsShow = 1 WHERE NotificationSetUpId = {notifyId}");

        /*var notifies = await _unitOfWork.Repository<NotificationSetUp>().Get().Include(x => x.NotifyMessages)
                .FirstOrDefaultAsync(x => x.Id == notifyId && x.Status == StatusList.Active.Id);
            if (notifies is null)
                return;
            foreach (var item in notifies.NotifyMessages!)
            {
                item.IsShow = true;
            }
            await _unitOfWork.SaveChangesAsync();
            await _hubContext.Clients.Groups(notifies.NotifyMessages.Select(x => x.UserId)!).UpdateNotify();*/
        if (affectRow > 0)
        {
            var notifyMessages =
                await connection.QueryAsync<NotifyMessage>(
                    $@"SELECT * FROM NotifyMessage WHERE NotificationSetUpId = {notifyId}");
            await _hubContext.Clients.Groups(notifyMessages.Select(x => x.UserId)!).UpdateNotify();
            await _notifySender.SendNotificationFcmForAll(notifyId);
        }
    }


    public async Task UpdatePointOfUserAsync(string columnName, int programId)
    {
        PointOfUserType programType = 0;
        var programName = "";
        DateTime date = default;
        await using var connection = new SqlConnection(_configuration["ConnectionString"]);
        switch (columnName)
        {
            case "Fortune":
                programType = PointOfUserType.RotationLuck;
                //var fortune = await _unitOfWork.Repository<Fortune>().Get().FirstOrDefaultAsync(x => x.Id == programId);
                var fortune =
                    await connection.QueryFirstOrDefaultAsync<Fortune>(
                        $@"SELECT * FROM Fortune WHERE Id = {programId}");
                programName = fortune.Descr;
                date = fortune.ToDate;
                break;
            case "Loyalty":
                programType = PointOfUserType.Qr;
                //var loyaltyProgram = await _unitOfWork.Repository<LoyaltyProgram>().Get().FirstOrDefaultAsync(x => x.Id == programId);
                var loyaltyProgram =
                    await connection.QueryFirstOrDefaultAsync<LoyaltyProgram>(
                        $@"SELECT * FROM LoyaltyProgram WHERE Id = {programId}");
                programName = loyaltyProgram.Name;
                date = loyaltyProgram.ExpirationDate;
                break;
        }

        var listPointOfUser =
            (await connection.QueryAsync<PointOfUser>(
                $@"SELECT * FROM PointOfUser WHERE SourceId = {programId} AND ProgramType = {programType}")).ToList();
        if (!listPointOfUser.Any())
            return;
        var listPointOfUserByUser = listPointOfUser.GroupBy(x => x.UserId).ToList();
        var listPointOfUserForInsert = new List<PointOfUser>();
        foreach (var item in listPointOfUserByUser)
        {
            var firstItem = item.FirstOrDefault()!;
            var totalPoint = item.Sum(x => x.Point);
            listPointOfUserForInsert.Add(new PointOfUser
            {
                Point = -totalPoint,
                Type = PointOfUserType.Expired,
                ProgramType = programType,
                SourceId = programId,
                UserId = item.Key,
                VendorId = firstItem.VendorId
            });
            await _mediator.Send(new CreateNotificationSetUp
            {
                Title = $"{-totalPoint} điểm tích lũy",
                SubTitle = date.ToUniversalTime().ToString(FormatDate.FormatDateDdMmYyyy),
                Type = NotifyType.PointsLoyalty,
                TimeStart = date,
                Content = $"Hết hạn sử dụng điểm {programName}",
                VendorId = firstItem.VendorId,
                Remark = "/earn-point-history",
                ToCurrentUser = false
            });
        }

        //await _unitOfWork.SaveChangesAsync();
        const string insertQuery =
            "INSERT INTO PointOfUser VALUES (@Id, @VendorId, @UserId, @Point, @Type, @ProgramType, @SourceId, @SourceDetailId, @ExpirationDate, @CreatedAt)";
        await connection.ExecuteAsync(insertQuery, listPointOfUserForInsert);
        await _hubContext.Clients.Groups(listPointOfUserByUser.Select(x => x.Key)!).UpdateNotify();
    }
}