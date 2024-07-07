using MediatR;
using Microsoft.Extensions.Logging;
using poc.core.api.net8.Interface; 
using Poc.Domain.Entities.Employee.Events; 
using Poc.Contract.Query.Employee.Interfaces; 
using Poc.Contract.Query.Employee.ViewModels;
using Poc.Contract.Query.Employee.Request;

namespace Poc.Command.Employee.Events; 

public class EmployeeCreatedEventHandler : INotificationHandler<EmployeeCreatedEvent>
{
    private readonly ILogger<EmployeeCreatedEventHandler> _logger;
    private readonly IEmployeeReadOnlyRepository _repo;
    private readonly IRedisCacheService<List<EmployeeQueryModel>> _cacheService;

    // Construtor que inicializa os campos privados com os parâmetros fornecidos
    public EmployeeCreatedEventHandler(ILogger<EmployeeCreatedEventHandler> logger,
                                   IEmployeeReadOnlyRepository repo,
                                   IRedisCacheService<List<EmployeeQueryModel>> cacheService)
    {
        _logger = logger;
        _repo = repo;
        _cacheService = cacheService;
    }

    // Método Handle que é executado quando o evento EmployeeCreatedEvent é disparado
    public async Task Handle(EmployeeCreatedEvent notification, CancellationToken cancellationToken)
    {
        // Chave do cache
        const string cacheKey = nameof(GetEmployeeQuery);

        // Remove a entrada de cache existente
        await _cacheService.DeleteAsync(cacheKey);

        // Recria a entrada de cache com os dados atualizados
        await _cacheService.GetOrCreateAsync(cacheKey, _repo.Get, TimeSpan.FromHours(2));
    }

    // responde ao evento de criação de um empregado invalidando e recriando o cache relacionado aos empregados para garantir que os dados mais recentes sejam usados nas consultas.
}