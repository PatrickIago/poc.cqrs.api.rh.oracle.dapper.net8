using Ardalis.Result;
using MediatR;
using Poc.Contract.Query.JobHistory.ViewModels;

namespace Poc.Contract.Query.JobHistory.Request;
public class GetJobHistoryQuery : IRequest<Result<List<JobHistoryQueryModel>>>
{
    public GetJobHistoryQuery()
    {

    }
}
