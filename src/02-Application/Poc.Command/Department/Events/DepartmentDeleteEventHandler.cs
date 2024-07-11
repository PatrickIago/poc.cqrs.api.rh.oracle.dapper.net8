using MediatR;
using Microsoft.Extensions.Logging;
using poc.core.api.net8.Interface;
using Poc.Contract.Query.Departament.Interfaces;
using Poc.Contract.Query.Departament.Request;
using Poc.Contract.Query.Departament.ViewModels;

public class DepartmentDeleteEventHandler : INotificationHandler<DepartmentDeletedEvent>
{
    private readonly ILogger<DepartmentDeleteEventHandler> _logger;
    private readonly IDepartmentReadOnlyRepository _repo;
    private readonly IRedisCacheService<List<DepartmentQueryModel>> _cacheService;

    public DepartmentDeleteEventHandler(ILogger<DepartmentDeleteEventHandler> logger, IDepartmentReadOnlyRepository repo, IRedisCacheService<List<DepartmentQueryModel>> cacheService)
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