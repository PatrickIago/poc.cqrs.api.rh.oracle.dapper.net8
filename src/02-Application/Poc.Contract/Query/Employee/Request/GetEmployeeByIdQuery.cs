using Ardalis.Result;
using MediatR;
using Poc.Contract.Query.Employee.ViewModels;
namespace Poc.Contract.Query.Employee.Request;
public class GetEmployeeByIdQuery : IRequest<Result<EmployeeQueryModel>>
{
    public GetEmployeeByIdQuery(decimal employeeId)
    {
        EmployeeId = employeeId;
    }

    public decimal EmployeeId { get; private set; }
}
