using Ardalis.Result;
using MediatR;
using Poc.Contract.Command.JobHistory.Response;

namespace Poc.Contract.Command.JobHistory.Request;
public class CreateJobHistoryCommand : IRequest<Result<CreateJobHistoryResponse>>
{
    public decimal EmployeeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string JobId { get; set; }
    public decimal DepartmentId { get; set; }
}
