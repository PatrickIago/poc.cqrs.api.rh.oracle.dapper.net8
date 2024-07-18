using MediatR;
using Microsoft.Extensions.Logging;
using poc.core.api.net8.Interface;
using Poc.Contract.Query.Job.Interfaces;
using Poc.Contract.Query.Job.ViewModels;
using Poc.Domain.Entities.Job.Events;

namespace Poc.Command.Job.Events;
public class JobCreatedEventHandler : INotificationHandler<JobCreatedEvent>
{
    private readonly ILogger<JobCreatedEventHandler> _logger;
    private readonly IJobReadOnlyRepository _repo;
    private readonly IRedisCacheService<List<JobQueryModel>> _cacheService;
    public JobCreatedEventHandler(ILogger<JobCreatedEventHandler> logger, IJobReadOnlyRepository repo, IRedisCacheService<List<JobQueryModel>> cacheService)
    {
        _logger = logger;
        _repo = repo;
        _cacheService = cacheService;
    }

    public async Task Handle(JobCreatedEvent notification, CancellationToken cancellationToken)
    {
        const string chacheKey = nameof(JobQueryModel);
        await _cacheService.DeleteAsync(chacheKey);
        await _cacheService.GetOrCreateAsync(chacheKey, _repo.Get, TimeSpan.FromHours(2));
    }
}
