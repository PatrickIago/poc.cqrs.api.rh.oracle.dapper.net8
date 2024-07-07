using Ardalis.Result;
using MediatR;
using Poc.Contract.Query.Employee.Interfaces;
using Poc.Contract.Query.Employee.Request;
using Poc.Contract.Query.Employee.ViewModels;
using Poc.Contract.Query.Region.Request;
using poc.core.api.net8.Interface;

namespace Poc.Query.Employee; 

public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, Result<List<EmployeeQueryModel>>>
{
    // Dependências que serão injetadas
    private readonly IEmployeeReadOnlyRepository _repo; // Repositório somente leitura para acessar dados de funcionários
    private readonly IRedisCacheService<List<EmployeeQueryModel>> _cacheService; // Serviço de cache para armazenar e recuperar dados em cache

    // Construtor para inicializar as dependências via injeção de dependência (DI)
    public GetEmployeeQueryHandler(IEmployeeReadOnlyRepository repo, IRedisCacheService<List<EmployeeQueryModel>> cacheService)
    {
        _repo = repo;
        _cacheService = cacheService;
    }

    // Método Handle que trata a requisição GetEmployeeQuery
    public async Task<Result<List<EmployeeQueryModel>>> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
    {
        const string cacheKey = nameof(GetEmployeeQuery); // Chave do cache para armazenar ou recuperar os dados

        // Tentando obter a lista de modelos do cache ou criando-a se não existir
        var employees = await _cacheService.GetOrCreateAsync(cacheKey, _repo.Get, TimeSpan.FromHours(2));

        // Retornando o resultado com sucesso
        return Result.Success(employees);
    }
}