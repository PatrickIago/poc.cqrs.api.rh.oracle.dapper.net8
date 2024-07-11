using Ardalis.Result;
using MediatR;

namespace Poc.Contract.Command.Employee.Request;
public class DeleteEmployeeCommand : IRequest<Result>
{
    public DeleteEmployeeCommand(decimal employeeId) => EmployeeId = employeeId;

    public decimal EmployeeId { get; private set; }
}
