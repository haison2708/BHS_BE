using BHS.API.Application.Commands.VendorCommand;
using BHS.API.Services;
using BHS.API.ViewModels;
using BHS.API.ViewModels.Vendor;
using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;

namespace BHS.API.Application.Queries.Vendor;

public class VendorQuery : BaseQuery, IVendorQuery
{
    private readonly IMediator _mediator;

    public VendorQuery(IConfiguration configuration, IIdentityService identityService, IMediator mediator) : base(
        configuration, identityService)
    {
        _mediator = mediator;
    }

    public async Task<object> GetAllAsync(QueryTemplate queryTemplate)
    {
        await using var connection = new SqlConnection(ConnectionString);
        var sql = Query.GetAllVendor(IdentityService.GetUserIdentity());
        var result = await connection.QueryAsync<VendorViewModel>(sql);
        return result.AsList();
    }

    public async Task<IList<VendorViewModel>> GetVendorsThatUserFollowsAsync()
    {
        await using var connection = new SqlConnection(ConnectionString);
        var sql = Query.GetVendorsThatUserFollows(IdentityService.GetUserIdentity());
        var result = await connection.QueryAsync<VendorViewModel>(sql);
        return result.AsList();
    }

    public async Task<IList<VendorViewModel>> GetVendorByNameAsync(string vendorName, bool byUser)
    {
        await using var connection = new SqlConnection(ConnectionString);
        var sql = Query.GetVendorsByName(IdentityService.GetUserIdentity(), vendorName, byUser);
        var result = await connection.QueryAsync<VendorViewModel>(sql);
        return result.AsList();
    }

    public async Task<object> GetAsync(int id)
    {
        var sql = Query.GetVendor(id, IdentityService.GetUserIdentity());
        await using var connection = new SqlConnection(ConnectionString);
        var result = await connection.QueryFirstOrDefaultAsync<VendorViewModel>(sql);
        return result;
    }


    public async Task<object> GetConfigRankOfVendorAsync()
    {
        await using var connection = new SqlConnection(ConnectionString);
        var sql = Query.GetConfigRankOfVendor(IdentityService.GetCurrentVendorId());

        var result = await connection.QueryFirstOrDefaultAsync<object>(sql);
        if (result is not null) return result;
        var setting = new CreateConfigRankOfVendor
        {
            VendorId = IdentityService.GetCurrentVendorId()
        };
        await _mediator.Send(setting);
        result = await connection.QueryFirstOrDefaultAsync<ConfigRankOfVendorViewModel>(sql);
        return result;
    }

    public async Task<object> GetLuckyWheelTurns()
    {
        await using var connection = new SqlConnection(ConnectionString);
        var sql = Query.GetLuckyWheelTurnsQuery(IdentityService.GetUserIdentity());
        return (await connection.QueryAsync<VendorViewModel>(sql)).AsList();
    }
}