using Ardalis.Result;
using MediatR;
using poc.core.api.net8.Interface;
using Poc.Contract.Query.Region.Interfaces;
using Poc.Contract.Query.Region.Request;
using Poc.Contract.Query.Region.ViewModels;

namespace Poc.Query.Region;

public class GetRegionQueryHandler : IRequestHandler<GetRegionQuery, Result<List<RegionQueryModel>>>
{
    private readonly IRegionsReadOnlyRepository _repo;
    private readonly IRedisCacheService<List<RegionQueryModel>> _cacheService;
    public GetRegionQueryHandler(IRegionsReadOnlyRepository repo, IRedisCacheService<List<RegionQueryModel>> cacheService)
    {
        _repo = repo;
        _cacheService = cacheService;
    }

    public async Task<Result<List<RegionQueryModel>>> Handle(GetRegionQuery request, CancellationToken cancellationToken)
    {
        const string cacheKey = nameof(GetRegionQuery);
        return Result.Success(await _cacheService.GetOrCreateAsync(cacheKey, _repo.Get, TimeSpan.FromHours(2)));
    }
}