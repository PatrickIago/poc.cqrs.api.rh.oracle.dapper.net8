using MediatR;
using Microsoft.Extensions.Logging;
using poc.core.api.net8.Interface;
using Poc.Contract.Query.Job.Interfaces;
using Poc.Contract.Query.Job.ViewModels;
using Poc.Domain.Entities.Job.Events;
namespace Poc.Command.Job.Events;
public class JobUpdatedEventHandler : INotificationHandler<JobUpdatedEvent>
{
    private readonly ILogger<JobUpdatedEventHandler> _logger;
    private readonly IJobReadOnlyRepository _repo;
    private readonly IRedisCacheService<List<JobQueryModel>> _cacheService;
    public JobUpdatedEventHandler(ILogger<JobUpdatedEventHandler> logger,
                                   IJobReadOnlyRepository repo,
                                   IRedisCacheService<List<JobQueryModel>> cacheService)
    {
        _logger = logger;
        _repo = repo;
        _cacheService = cacheService;
    }

    public async Task Handle(JobUpdatedEvent notification, CancellationToken cancellationToken)
    {
        const string cacheKey = nameof(JobQueryModel);
        await _cacheService.DeleteAsync(cacheKey);
        await _cacheService.GetOrCreateAsync(cacheKey, _repo.Get, TimeSpan.FromHours(2));
    }
}
