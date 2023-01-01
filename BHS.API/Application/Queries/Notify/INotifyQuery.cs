using BHS.API.ViewModels;
using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;

namespace BHS.API.Application.Queries.Notify;

public interface INotifyQuery : IQuery
{
    Task<object> GetAllNotifyMessageAsync(NotifyType type, QueryTemplate queryTemplate);
}