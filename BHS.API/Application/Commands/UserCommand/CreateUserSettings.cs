using BHS.API.ViewModels.Users;
using MediatR;

namespace BHS.API.Application.Commands.UserCommand;

public class CreateUserSettings : IRequest<UserSettingsViewModel>
{
    public bool IsFingerprintLogin { get; set; }
    public bool IsGetNotifications { get; set; }
    public string? LangId { get; set; }
    public int? VendorId { get; set; }
}