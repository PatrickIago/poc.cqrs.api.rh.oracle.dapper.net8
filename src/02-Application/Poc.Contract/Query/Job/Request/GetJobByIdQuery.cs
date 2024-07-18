using Ardalis.Result;
using MediatR;
using Poc.Contract.Query.Job.ViewModels;

namespace Poc.Contract.Query.Job.Request;
public class GetJobByIdQuery : IRequest<Result<JobQueryModel>>
{
    public GetJobByIdQuery(string jobId)
    {
        JobId = jobId;
    }
    public string JobId { get; private set; }
}
