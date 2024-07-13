using Ardalis.Result;
using MediatR;
using Poc.Contract.Query.JobHistory.ViewModels;

namespace Poc.Contract.Query.JobHistory.Request
{
    public class GetJobHistoryByIdQuery : IRequest<Result<JobHistoryQueryModel>>
    {
        public GetJobHistoryByIdQuery(decimal employeeId)
        {
            EmployeeId = employeeId;
        }
        public decimal EmployeeId { get; private set; }
    }
}
