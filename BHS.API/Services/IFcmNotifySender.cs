using System.Text;
using BHS.Domain.Entities.Notify;
using BHS.Domain.Entities.Users;
using BHS.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BHS.API.Services;

public interface IFcmNotifySender : ITransientService
{
    Task SendNotificationFcmForAll(int id);
}

public class FcmNotifySender : IFcmNotifySender
{
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWork _unitOfWork;

    public FcmNotifySender(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    public async Task SendNotificationFcmForAll(int id)
    {
        var listMessage = await _unitOfWork.Repository<NotifyMessage>().Get().Include(x => x.NotificationSetUp)
            .Include(x => x.User).ThenInclude(x => x!.UserAppToken).Where(x => x.NotificationSetUpId == id)
            .ToListAsync();
        foreach (var item in listMessage)
        {
            if (item.User!.UserAppToken is null)
                continue;
            var listToken = item.User.UserAppToken.Select(x => x.Token).ToArray();
            var sendNotificationFcm = SendNotificationFcm(listToken!, item);
        }
    }

    private async Task SendNotificationFcm(string[] listToken, NotifyMessage item)
    {
        var notifyMessageIds = new List<int>();
        var tokens = new List<string>();
        using (var httpClient = new HttpClient())
        {
            var bodyFcm = new BodyFcm
            {
                registration_ids = listToken,
                priority = "High",
                content_available = true,
                notification = new Notification
                {
                    title = item.NotificationSetUp!.Title,
                    body = item.NotificationSetUp.Content,
                    icon = "https://dev.hqsoft.vn/1CXAPP/assets/icon/1CX-favicon.png"
                },
                data = new Data
                {
                    url = item.NotificationSetUp.Remark
                }
            };
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization",
                "key=" + _configuration["FirebaseKey"]);
            var response = await httpClient.PostAsync("https://fcm.googleapis.com/fcm/send",
                new StringContent(JsonConvert.SerializeObject(bodyFcm), Encoding.UTF8, "application/json"));
            var res = await response.Content.ReadAsStringAsync();
            ResponseFcm? responseFcm = null;
            if (response.IsSuccessStatusCode) responseFcm = JsonConvert.DeserializeObject<ResponseFcm>(res);

            if (responseFcm != null)
            {
                if (responseFcm.Success > 0)
                    notifyMessageIds.Add(item.Id);
                if (responseFcm.Failure > 0)
                {
                    var lstIndex = responseFcm.Results!.Select((x, i) => new { e = x, index = i })
                        .Where(x => x.e.Error is not null)
                        .Select(x => x.index);
                    tokens.AddRange(listToken.Select((x, i) => new { e = x, index = i })
                        .Where(x => lstIndex.Any(i => i == x.index))
                        .Select(x => x.e));
                }
            }

            var listNotifyMessageForUpdate = await _unitOfWork.Repository<NotifyMessage>().Get().Where(x =>
                notifyMessageIds.Any(t => x.Id == t)).ToListAsync();
            var listTokenForDelete =
                await _unitOfWork.Repository<UserAppToken>().Get().Where(x => tokens.Any(t => t == x.Token))
                    .ToListAsync();
            _unitOfWork.Repository<UserAppToken>()
                .DeleteRange(listTokenForDelete);
            _unitOfWork.Repository<NotifyMessage>().UpdateRange(listNotifyMessageForUpdate);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}

public class BodyFcm
{
    public string[]? registration_ids { get; set; }
    public string? priority { get; set; }
    public bool? content_available { get; set; }
    public Notification? notification { get; set; }
    public Data? data { get; set; }
}

public class Notification
{
    public string? title { get; set; }
    public string? body { get; set; }
    public string? icon { get; set; }
}

public class Data
{
    //public string? Remark { get; set; }
    /*public string type { get; set; }*/
    public string? url { get; set; }
}

public class ResponseFcm
{
    public long Multicast_Id { get; set; }
    public int Success { get; set; }
    public int Failure { get; set; }
    public List<Results>? Results { get; set; }
}

public class Results
{
    public string? Error { get; set; }
    public string? Message_Id { get; set; }
}