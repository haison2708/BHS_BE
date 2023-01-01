using BHS.Domain.Entities.Users;
using BHS.Domain.SeedWork;

namespace BHS.API.Services;

public interface IIdentityService : ITransientService
{
    string GetUserIdentity();
    string GetUserName();
    bool IsAuthenticated();
    string GetAccessToken();

    string GetLangId();

    //TimeZoneInfo GetTimeZone();
    string GetToken();
    int GetCurrentVendorId();
}

public class IdentityService : IIdentityService
{
    private readonly IHttpContextAccessor _context;
    private readonly IUnitOfWork _unitOfWork;

    public IdentityService(IHttpContextAccessor context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public string GetToken()
    {
        _context.HttpContext!.Request.Headers.TryGetValue("Authorization", out var token);
        return token;
    }

    public int GetCurrentVendorId()
    {
        return Convert.ToInt32(_unitOfWork.Repository<UserSettings>().Get()
            .FirstOrDefault(x => x.UserId == GetUserIdentity())!.VendorId);
    }

    public string GetUserIdentity()
    {
        return _context.HttpContext!.User.FindFirst("sub")!.Value;
    }

    public string GetUserName()
    {
        return _context.HttpContext!.User.Identity!.Name!;
    }

    public bool IsAuthenticated()
    {
        return _context.HttpContext!.User.Identity!.IsAuthenticated;
    }

    public string GetAccessToken()
    {
        var value = _context.HttpContext!.Request.Headers["Authorization"].ToString();
        if (string.IsNullOrWhiteSpace(value))
            value = _context.HttpContext.Request.Headers["AccessToken"].ToString();
        return value;
    }

    public string GetLangId()
    {
        return _unitOfWork.Repository<UserSettings>().Get().FirstOrDefault(x => x.UserId == GetUserIdentity())!.LangId!;
    }

    public TimeZoneInfo GetTimeZone()
    {
        var timeZoneId = _context.HttpContext!.Request.Headers["TimeZone"].FirstOrDefault();
        var timeZone = TimeZoneInfo.GetSystemTimeZones().FirstOrDefault(x => x.Id == timeZoneId);
        return timeZone ?? TimeZoneInfo.Local;
    }
}