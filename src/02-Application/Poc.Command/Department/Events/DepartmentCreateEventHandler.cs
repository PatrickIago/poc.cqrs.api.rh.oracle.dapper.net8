using MediatR;
using Microsoft.Extensions.Logging;
using poc.core.api.net8.Interface;
using Poc.Contract.Query.Departament.Interfaces;
using Poc.Contract.Query.Departament.Request;
using Poc.Contract.Query.Departament.ViewModels;
using Poc.Domain.Entities.Department.Events;

namespace Poc.Command.Departament.Events;
public class DepartmentCreateEventHandler : INotificationHandler<DepartmentCreatedEvent>
{
    private readonly ILogger<DepartmentCreateEventHandler> _logger;
    private readonly IDepartmentReadOnlyRepository _repo;
    private readonly IRedisCacheService<List<DepartmentQueryModel>> _cacheService;

    public DepartmentCreateEventHandler(ILogger<DepartmentCreateEventHandler> logger, IDepartmentReadOnlyRepository repo, IRedisCacheService<List<DepartmentQueryModel>> cacheService)
    {
        _logger = logger;
        _repo = repo;
        _cacheService = cacheService;
    }

    public async Task Handle(DepartmentCreatedEvent notification, CancellationToken cancellationToken)
    {
        // Chave do cache
        const string cacheKey = nameof(GetDepartmentQuery);

        // Remove a entrada de cache existente
        await _cacheService.DeleteAsync(cacheKey);

        // Recria a entrada de cache com os dados atualizados
        await _cacheService.GetOrCreateAsync(cacheKey, _repo.Get, TimeSpan.FromHours(2));
    }
}
// O DepartamentCreatedEventHandler lida com o evento de criação de departamento, removendo e recriando a entrada de cache com os dados atualizados após a criação de um departamento.