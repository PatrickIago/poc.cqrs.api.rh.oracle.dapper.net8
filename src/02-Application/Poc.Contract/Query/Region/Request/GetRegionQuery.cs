using Ardalis.Result;
using MediatR;
using Poc.Contract.Query.Region.ViewModels;

namespace Poc.Contract.Query.Region.Request;

public class GetRegionQuery : IRequest<Result<List<RegionQueryModel>>>
{
    public GetRegionQuery()
    {
    }
}