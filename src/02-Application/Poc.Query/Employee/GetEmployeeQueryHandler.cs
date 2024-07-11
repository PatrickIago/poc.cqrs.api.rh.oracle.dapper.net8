using Ardalis.Result;
using MediatR;
using poc.core.api.net8.Interface;
using Poc.Contract.Query.Employee.Interfaces;
using Poc.Contract.Query.Employee.Request;
using Poc.Contract.Query.Employee.ViewModels;

namespace Poc.Query.Employee;

public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, Result<List<EmployeeQueryModel>>>
{
    private readonly IEmployeeReadOnlyRepository _repo;
    private readonly IRedisCacheService<List<EmployeeQueryModel>> _cacheService; // Serviço de cache para armazenar e recuperar dados em cache

    public GetEmployeeQueryHandler(IEmployeeReadOnlyRepository repo, IRedisCacheService<List<EmployeeQueryModel>> cacheService)
    {
        _repo = repo;
        _cacheService = cacheService;
    }

    // Método Handle que trata a requisição GetEmployeeQuery
    public async Task<Result<List<EmployeeQueryModel>>> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
    {
        const string cacheKey = nameof(GetEmployeeQuery);

        return Result.Success(await _cacheService.GetOrCreateAsync(cacheKey, _repo.Get, TimeSpan.FromHours(2)));
    }
}