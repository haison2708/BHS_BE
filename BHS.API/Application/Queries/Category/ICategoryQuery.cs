using BHS.API.ViewModels;
using BHS.Domain.SeedWork;

namespace BHS.API.Application.Queries.Category;

public interface ICategoryQuery : IQuery
{
    Task<object> GetAllAsync(QueryTemplate queryTemplate);
}