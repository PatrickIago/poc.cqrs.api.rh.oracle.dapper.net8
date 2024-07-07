using Ardalis.Result;
using MediatR;
using poc.core.api.net8.Interface;
using Poc.Contract.Query.Departament.Interfaces;
using Poc.Contract.Query.Departament.Request;
using Poc.Contract.Query.Departament.ViewModels;

namespace Poc.Query.Departament;
public class GetDepartmentQueryHandler : IRequestHandler<GetDepartmentQuery, Result<List<DepartmentQueryModel>>>
{
    private readonly IDepartmentReadOnlyRepository _repo;
    private readonly IRedisCacheService<List<DepartmentQueryModel>> _cacheService;

    public GetDepartmentQueryHandler(IDepartmentReadOnlyRepository repo, IRedisCacheService<List<DepartmentQueryModel>> cacheService)
    {
        _repo = repo;
        _cacheService = cacheService;
    }

    public async Task<Result<List<DepartmentQueryModel>>> Handle(GetDepartmentQuery request, CancellationToken cancellationToken)
    {
        {
            const string cacheKey = nameof(GetDepartmentQuery); // Chave do cache para armazenar ou recuperar os dados

            // Tentando obter a lista de modelos do cache ou criando-a se não existir
            var departaments = await _cacheService.GetOrCreateAsync(cacheKey, _repo.Get, TimeSpan.FromHours(2));

            // Retornando o resultado com sucesso
            return Result.Success(departaments);
        }
    }
}
