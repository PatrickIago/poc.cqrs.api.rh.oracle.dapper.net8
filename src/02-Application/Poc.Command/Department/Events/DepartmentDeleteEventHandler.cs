using MediatR;
using Microsoft.Extensions.Logging;
using Poc.Contract.Query.Employee.Interfaces;
using Poc.Contract.Query.Employee.ViewModels;
using poc.core.api.net8.Interface;
using Poc.Contract.Query.Departament.Request;

namespace Poc.Command.Departament.Events;
public class DepartmentDeleteEventHandler : INotificationHandler<DepartmentDeletedEvent>
{
    private readonly ILogger<DepartmentDeleteEventHandler> _logger;
    private readonly IEmployeeReadOnlyRepository _repo;
    private readonly IRedisCacheService<List<EmployeeQueryModel>> _cacheService;

    public DepartmentDeleteEventHandler(ILogger<DepartmentDeleteEventHandler> logger, IEmployeeReadOnlyRepository repo, IRedisCacheService<List<EmployeeQueryModel>> cacheService)
    {
        _logger = logger;
        _repo = repo;
        _cacheService = cacheService;
    }

    public async Task Handle(DepartmentDeletedEvent notification, CancellationToken cancellationToken)
    {
        const string cacheKey = nameof(GetDepartmentQuery);
        await _cacheService.DeleteAsync(cacheKey);
        await _cacheService.GetOrCreateAsync(cacheKey, _repo.Get, TimeSpan.FromHours(2));
    }
}
