using System.Data.SqlClient;
using BHS.API.Services;
using BHS.API.ViewModels.Languages;
using BHS.Domain.SeedWork;
using Dapper;

namespace BHS.API.Application.Queries.Language;

public interface ILanguageQuery : IQuery
{
    Task<object> GetAllLanguageAsync();
}

public class LanguageQuery : BaseQuery, ILanguageQuery
{
    public LanguageQuery(IConfiguration configuration, IIdentityService identityService) : base(configuration,
        identityService)
    {
    }

    public async Task<object> GetAllLanguageAsync()
    {
        var sql = @"select * from Language";
        await using var connection = new SqlConnection(ConnectionString);
        var result = await connection.QueryAsync<LanguageViewModel>(sql);
        return result.AsList();
    }
}