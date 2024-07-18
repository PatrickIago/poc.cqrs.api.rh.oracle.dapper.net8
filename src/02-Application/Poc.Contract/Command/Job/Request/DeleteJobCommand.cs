using Ardalis.Result;
using MediatR;
namespace Poc.Contract.Command.Job.Request;

public class DeleteJobCommand : IRequest<Result>
{
    public DeleteJobCommand(string jobId) => JobId = jobId;

    public string JobId { get; private set; }
}
