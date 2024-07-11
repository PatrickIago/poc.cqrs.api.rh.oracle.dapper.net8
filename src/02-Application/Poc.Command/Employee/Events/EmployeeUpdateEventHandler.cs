using MediatR;
using Microsoft.Extensions.Logging;
using poc.core.api.net8.Interface;
using Poc.Contract.Query.Employee.Interfaces;
using Poc.Contract.Query.Employee.ViewModels;

namespace Poc.Command.Employee.Events;
public class EmployeeUpdateEventHandler : INotificationHandler<EmployeeUpdatedEvent>
{
    private readonly ILogger<EmployeeUpdateEventHandler> _logger;
    private readonly IEmployeeReadOnlyRepository _repo;
    private readonly IRedisCacheService<List<EmployeeQueryModel>> _cacheService;
    public EmployeeUpdateEventHandler(ILogger<EmployeeUpdateEventHandler> logger,
                                   IEmployeeReadOnlyRepository repo,
                                   IRedisCacheService<List<EmployeeQueryModel>> cacheService)
    {
        _logger = logger;
        _repo = repo;
        _cacheService = cacheService;
    }

    public async Task Handle(EmployeeUpdatedEvent notification, CancellationToken cancellationToken)
    {
        const string cacheKey = nameof(EmployeeQueryModel);
        await _cacheService.DeleteAsync(cacheKey);
        await _cacheService.GetOrCreateAsync(cacheKey, _repo.Get, TimeSpan.FromHours(2));
    }
}
