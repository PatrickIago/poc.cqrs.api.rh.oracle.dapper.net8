using MediatR;
using Microsoft.Extensions.Logging;
using poc.core.api.net8.Interface;
using Poc.Contract.Query.Region.Interfaces;
using Poc.Contract.Query.Region.Request;
using Poc.Contract.Query.Region.ViewModels;
using Poc.Domain.Entities.Region.Events;

namespace Poc.Command.Region.Events;

public class RegionCreatedEventHandler : INotificationHandler<RegionCreatedEvent>
{
    private readonly ILogger<RegionCreatedEventHandler> _logger;
    private readonly IRegionsReadOnlyRepository _repo;
    private readonly IRedisCacheService<List<RegionQueryModel>> _cacheService;
    public RegionCreatedEventHandler(ILogger<RegionCreatedEventHandler> logger,
                                   IRegionsReadOnlyRepository repo,
                                   IRedisCacheService<List<RegionQueryModel>> cacheService)
    {
        _logger = logger;
        _repo = repo;
        _cacheService = cacheService;
    }

    public async Task Handle(RegionCreatedEvent notification, CancellationToken cancellationToken)
    {
        const string cacheKey = nameof(GetRegionQuery);
        await _cacheService.DeleteAsync(cacheKey);
        await _cacheService.GetOrCreateAsync(cacheKey, _repo.Get, TimeSpan.FromHours(2));
    }
}
