using MediatR;
using Microsoft.Extensions.Logging;
using poc.core.api.net8.Interface;
using Poc.Contract.Query.JobHistory.Interfaces;
using Poc.Contract.Query.JobHistory.Request;
using Poc.Contract.Query.JobHistory.ViewModels;
using Poc.Domain.Entities.JobHistory.Events;
namespace Poc.Command.JobHistory.Events;
public class JobHistoryUpdateEventHandler : INotificationHandler<JobHistoryUpdatedEvent>
{
    private ILogger<JobHistoryUpdateEventHandler> _logger;
    private readonly IJobHistoryReadOnlyRepository _repo;
    private readonly IRedisCacheService<List<JobHistoryQueryModel>> _cacheService;

    public JobHistoryUpdateEventHandler(ILogger<JobHistoryUpdateEventHandler> logger, IJobHistoryReadOnlyRepository repo, IRedisCacheService<List<JobHistoryQueryModel>> cacheService)
    {
        _logger = logger;
        _repo = repo;
        _cacheService = cacheService;
    }

    public async Task Handle(JobHistoryUpdatedEvent notification, CancellationToken cancellationToken)
    {
        const string cacheKey = nameof(GetJobHistoryQuery);
        await _cacheService.DeleteAsync(cacheKey);
        await _cacheService.GetOrCreateAsync(cacheKey, _repo.Get, TimeSpan.FromHours(2));
    }
}
