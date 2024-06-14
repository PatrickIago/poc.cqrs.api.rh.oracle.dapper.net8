using Ardalis.Result;
using MediatR;
using Poc.Contract.Query.Region.ViewModels;

namespace Poc.Contract.Query.Region.Request;

public class GetRegionByIdQuery : IRequest<Result<RegionQueryModel>>
{
    public GetRegionByIdQuery(decimal regionId)
    {
        RegionId = regionId;
    }
    public decimal RegionId { get; private set; }
}