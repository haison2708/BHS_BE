using BHS.API.ViewModels;
using BHS.Domain.SeedWork;

namespace BHS.API.Application.Queries.Fortune;

public interface IFortuneQuery : IQuery
{
    Task<object> GetAllAsync(int vendorId, QueryTemplate queryTemplate);
    Task<object> GetAsync(int id);
}