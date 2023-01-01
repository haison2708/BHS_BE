using BHS.API.Services;

namespace BHS.API.Application.Queries;

public class BaseQuery
{
    protected readonly string ConnectionString;
    protected readonly IIdentityService IdentityService;


    protected BaseQuery(IConfiguration configuration, IIdentityService identityService)
    {
        IdentityService = identityService;
        ConnectionString = configuration["ConnectionString"];
    }
}