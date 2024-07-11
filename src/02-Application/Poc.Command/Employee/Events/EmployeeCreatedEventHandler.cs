using MediatR;
using Microsoft.Extensions.Logging;
using poc.core.api.net8.Interface;
using Poc.Contract.Query.Employee.Interfaces;
using Poc.Contract.Query.Employee.ViewModels;

namespace Poc.Command.Employee.Events;
public class EmployeeCreatedEventHandler : INotificationHandler<EmployeeCreatedEvent>
{
    private readonly ILogger<EmployeeCreatedEventHandler> _logger;
    private readonly IEmployeeReadOnlyRepository _repo;
    private readonly IRedisCacheService<List<EmployeeQueryModel>> _cacheService;
    public EmployeeCreatedEventHandler(ILogger<EmployeeCreatedEventHandler> logger, IEmployeeReadOnlyRepository repo, IRedisCacheService<List<EmployeeQueryModel>> cacheService)
    {
        _logger = logger;
        _repo = repo;
        _cacheService = cacheService;
    }

    public async Task Handle(EmployeeCreatedEvent notification, CancellationToken cancellationToken)
    {
        const string chacheKey = nameof(EmployeeQueryModel);
        await _cacheService.DeleteAsync(chacheKey);
        await _cacheService.GetOrCreateAsync(chacheKey, _repo.Get, TimeSpan.FromHours(2));
    }
}
