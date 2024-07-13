using Ardalis.Result;
using MediatR;

namespace Poc.Contract.Command.JobHistory.Request;
public class UpdateJobHistoryCommand : IRequest<Result>
{
    public decimal EmployeeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string JobId { get; set; }
    public decimal DepartmentId { get; set; }
}
