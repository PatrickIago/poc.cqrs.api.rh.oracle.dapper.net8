using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using MediatR;
using poc.core.api.net8.Interface;
using Poc.Contract.Query.Departament.Interfaces;
using Poc.Contract.Query.Departament.Request;
using Poc.Contract.Query.Departament.Validators;
using Poc.Contract.Query.Departament.ViewModels;

namespace Poc.Query.Departament;
public class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdQuery, Result<DepartmentQueryModel>>
{

    private readonly IDepartmentReadOnlyRepository _repo;
    private readonly IRedisCacheService<DepartmentQueryModel> _cacheService;
    private readonly GetDepartmentByIdQueryValidator _validator;

    public GetDepartmentByIdQueryHandler(IDepartmentReadOnlyRepository repo, IRedisCacheService<DepartmentQueryModel> cacheService, GetDepartmentByIdQueryValidator validator)
    {
        _repo = repo;
        _cacheService = cacheService;
        _validator = validator;
    }

    public async Task<Result<DepartmentQueryModel>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
    {
        // Validando a requisição
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors()); // Retorna resultado inválido se a validação falhar

        // Gerando a chave do cache
        var cacheKey = $"{nameof(GetDepartmentByIdQuery)}_{request.Id}";

        // Tentando obter o modelo do cache ou criando-o se não existir
        var model = await _cacheService.GetOrCreateAsync(cacheKey, () => _repo.Get(request.Id), TimeSpan.FromHours(2));

        // Verificando se o modelo foi encontrado
        if (model == null)
            return Result.NotFound($"Nenhum registro encontrado pelo Id: {request.Id}"); // Retorna resultado "não encontrado"

        // Retornando o resultado com sucesso
        return Result.Success(model);
    }
}
