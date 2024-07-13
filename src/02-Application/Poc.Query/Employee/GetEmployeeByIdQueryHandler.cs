using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using MediatR;
using poc.core.api.net8.Interface;
using Poc.Contract.Query.Employee.Interfaces;
using Poc.Contract.Query.Employee.Request;
using Poc.Contract.Query.Employee.Validators;
using Poc.Contract.Query.Employee.ViewModels;

namespace Poc.Query.Employee;
public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Result<EmployeeQueryModel>>
{
    private readonly IEmployeeReadOnlyRepository _repo;
    private readonly IRedisCacheService<EmployeeQueryModel> _cacheService;
    private readonly GetEmployeeByIdQueryValidator _validator;

    public GetEmployeeByIdQueryHandler(IEmployeeReadOnlyRepository repo, IRedisCacheService<EmployeeQueryModel> cacheService, GetEmployeeByIdQueryValidator validator)
    {
        _repo = repo;
        _cacheService = cacheService;
        _validator = validator;
    }

    public async Task<Result<EmployeeQueryModel>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());

        var cacheKey = $"{nameof(GetEmployeeByIdQuery)}_{request.EmployeeId}";

        var model = await _cacheService.GetOrCreateAsync(cacheKey, () => _repo.Get(request.EmployeeId), TimeSpan.FromHours(2));

        return Result.Success(model);
    } 
}
