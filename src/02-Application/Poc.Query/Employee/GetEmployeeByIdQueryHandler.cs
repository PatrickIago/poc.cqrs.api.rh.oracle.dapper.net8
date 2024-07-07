using Ardalis.Result;
using MediatR;
using Poc.Contract.Query.Employee.Interfaces;
using Poc.Contract.Query.Employee.Request;
using Poc.Contract.Query.Employee.Validators;
using Poc.Contract.Query.Employee.ViewModels;
using poc.core.api.net8.Interface;
using Ardalis.Result.FluentValidation;

namespace Poc.Query.Employee; 

public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Result<EmployeeQueryModel>>
{
    // Dependências que serão injetadas
    private readonly IEmployeeReadOnlyRepository _repo; // Repositório somente leitura para acessar dados de funcionários
    private readonly IRedisCacheService<EmployeeQueryModel> _cacheService; // Serviço de cache para armazenar e recuperar dados em cache
    private readonly GetEmployeeByIdQueryValidator _validator; // Validador para a requisição GetEmployeeByIdQuery

    // Construtor para inicializar as dependências via injeção de dependência (DI)
    public GetEmployeeByIdQueryHandler(
        IEmployeeReadOnlyRepository repo,
        IRedisCacheService<EmployeeQueryModel> cacheService,
        GetEmployeeByIdQueryValidator validator)
    {
        _repo = repo;
        _cacheService = cacheService;
        _validator = validator;
    }

    // Método Handle que trata a requisição GetEmployeeByIdQuery
    public async Task<Result<EmployeeQueryModel>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        // Validando a requisição
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors()); // Retorna resultado inválido se a validação falhar

        // Gerando a chave do cache
        var cacheKey = $"{nameof(GetEmployeeByIdQuery)}_{request.Id}";

        // Tentando obter o modelo do cache ou criando-o se não existir
        var model = await _cacheService.GetOrCreateAsync(cacheKey, () => _repo.Get(request.Id), TimeSpan.FromHours(2));

        // Verificando se o modelo foi encontrado
        if (model == null)
            return Result.NotFound($"Nenhum registro encontrado pelo Id: {request.Id}"); // Retorna resultado "não encontrado"

        // Retornando o resultado com sucesso
        return Result.Success(model);
    }
}