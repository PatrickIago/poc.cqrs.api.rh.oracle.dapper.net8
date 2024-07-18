using Ardalis.Result;
using MediatR;
using Poc.Contract.Query.Job.ViewModels;

namespace Poc.Contract.Query.Job.Request;
public class GetJobQuery : IRequest<Result<List<JobQueryModel>>>
{
    public GetJobQuery()
    {

    }
}
