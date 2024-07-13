using Ardalis.Result;
using MediatR;
using poc.core.api.net8.Interface;
using Poc.Contract.Query.JobHistory.Interfaces;
using Poc.Contract.Query.JobHistory.Request;
using Poc.Contract.Query.JobHistory.ViewModels;

namespace Poc.Query.JobHistory;
public class GetJobHistoryQueryHandler : IRequestHandler<GetJobHistoryQuery, Result<List<JobHistoryQueryModel>>>
{
    private readonly IJobHistoryReadOnlyRepository _repo;
    private readonly IRedisCacheService<List<JobHistoryQueryModel>> _cacheService;

    public GetJobHistoryQueryHandler(IJobHistoryReadOnlyRepository repo, IRedisCacheService<List<JobHistoryQueryModel>> cacheService)
    {
        _repo = repo;
        _cacheService = cacheService;
    }

    public async Task<Result<List<JobHistoryQueryModel>>> Handle(GetJobHistoryQuery request, CancellationToken cancellationToken)
    {
        const string cacheKey = nameof(JobHistoryQueryModel);

        return Result.Success(await _cacheService.GetOrCreateAsync(cacheKey, _repo.Get, TimeSpan.FromHours(2)));
    }
}
