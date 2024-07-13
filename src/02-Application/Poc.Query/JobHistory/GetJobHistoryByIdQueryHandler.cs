using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using MediatR;
using poc.core.api.net8.Interface;
using Poc.Contract.Query.JobHistory.Interfaces;
using Poc.Contract.Query.JobHistory.Request;
using Poc.Contract.Query.JobHistory.Validators;
using Poc.Contract.Query.JobHistory.ViewModels;
namespace Poc.Query.JobHistory;
public class GetJobHistoryByIdQueryHandler : IRequestHandler<GetJobHistoryByIdQuery, Result<JobHistoryQueryModel>>
{
    private readonly IJobHistoryReadOnlyRepository _repo;
    private readonly IRedisCacheService<JobHistoryQueryModel> _cacheService;
    private readonly GetJobHistoryByIdQueryValidator _validator;

    public GetJobHistoryByIdQueryHandler(IJobHistoryReadOnlyRepository repo, IRedisCacheService<JobHistoryQueryModel> cacheService, GetJobHistoryByIdQueryValidator validator)
    {
        _repo = repo;
        _cacheService = cacheService;
        _validator = validator;
    }

    public async Task<Result<JobHistoryQueryModel>> Handle(GetJobHistoryByIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());

        var cacheKey = $"{nameof(GetJobHistoryByIdQuery)}_{request.EmployeeId}";

        var model = await _cacheService.GetOrCreateAsync(cacheKey, () => _repo.Get(request.EmployeeId), TimeSpan.FromHours(2));

        return Result.Success(model);

    }
}
