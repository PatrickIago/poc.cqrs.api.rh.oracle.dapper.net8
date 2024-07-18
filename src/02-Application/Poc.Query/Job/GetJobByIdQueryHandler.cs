using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using MediatR;
using poc.core.api.net8.Interface;
using Poc.Contract.Query.Job.Interfaces;
using Poc.Contract.Query.Job.Request;
using Poc.Contract.Query.Job.Validators;
using Poc.Contract.Query.Job.ViewModels;

namespace Poc.Query.Job;
public class GetJobByIdQueryHandler : IRequestHandler<GetJobByIdQuery, Result<JobQueryModel>>
{
    private readonly IJobReadOnlyRepository _repo;
    private readonly IRedisCacheService<JobQueryModel> _cacheService;
    private readonly GetJobByIdQueryValidator _validator;

    public GetJobByIdQueryHandler(IJobReadOnlyRepository repo, IRedisCacheService<JobQueryModel> cacheService, GetJobByIdQueryValidator validator)
    {
        _repo = repo;
        _cacheService = cacheService;
        _validator = validator;
    }

    public async Task<Result<JobQueryModel>> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());

        var cacheKey = $"{nameof(GetJobByIdQuery)}_{request.JobId}";

        var model = await _cacheService.GetOrCreateAsync(cacheKey, () => _repo.Get(request.JobId), TimeSpan.FromHours(2));

        return Result.Success(model);
    }
}
