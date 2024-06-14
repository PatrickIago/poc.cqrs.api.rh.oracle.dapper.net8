using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Poc.Contract.Query.Region.Request;
using Poc.Contract.Query.Region.Validators;
using Poc.Contract.Query.Region.ViewModels;
using Poc.Query.Region;

namespace Poc.Query;

public class QueryInitializer
{
    public static void Initialize(IServiceCollection services)
    {
        #region Oracle
        services.AddTransient<IRequestHandler<GetRegionQuery, Result<List<RegionQueryModel>>>, GetRegionQueryHandler>();
        services.AddTransient<IRequestHandler<GetRegionByIdQuery, Result<RegionQueryModel>>, GetRegionByIdQueryHandler>();
        services.AddTransient<GetRegionByIdQueryValidator>();
        #endregion
    }
}
