using Ardalis.Result;
using MediatR;

namespace Poc.Contract.Command.JobHistory.Request;
public class DeleteJobHistoryCommand : IRequest<Result>
{
    public DeleteJobHistoryCommand(decimal employeeId) => EmployeeId = employeeId;
    public decimal EmployeeId { get; private set; }
}
