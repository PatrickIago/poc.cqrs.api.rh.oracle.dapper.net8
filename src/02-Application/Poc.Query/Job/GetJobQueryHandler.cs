using Ardalis.Result;
using MediatR;
using poc.core.api.net8.Interface;
using Poc.Contract.Query.Job.Interfaces;
using Poc.Contract.Query.Job.Request;
using Poc.Contract.Query.Job.ViewModels;

namespace Poc.Query.Job;
public class GetJobQueryHandler : IRequestHandler<GetJobQuery, Result<List<JobQueryModel>>>
{
    private readonly IJobReadOnlyRepository _repo;
    private readonly IRedisCacheService<List<JobQueryModel>> _cacheService;

    public GetJobQueryHandler(IJobReadOnlyRepository repo, IRedisCacheService<List<JobQueryModel>> cacheService)
    {
        _repo = repo;
        _cacheService = cacheService;
    }
    public async Task<Result<List<JobQueryModel>>> Handle(GetJobQuery request, CancellationToken cancellationToken)
    {
        const string cacheKey = nameof(GetJobQuery);
        return Result.Success(await _cacheService.GetOrCreateAsync(cacheKey, _repo.Get, TimeSpan.FromHours(2)));
    }
}
