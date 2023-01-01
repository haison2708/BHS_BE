using BHS.Domain.Entities.Users;
using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities.Languages;

public class Language : Entity<string>, IAggregateRoot
{
    public string? LanguageName { get; set; }
    public IList<UserSettings>? UserSettings { get; set; }
}