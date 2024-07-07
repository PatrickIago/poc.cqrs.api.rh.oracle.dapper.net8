using Ardalis.Result;
using MediatR;

namespace Poc.Contract.Command.Employee.Request;
public class DeleteEmployeeCommand : IRequest<Result>
{
    public DeleteEmployeeCommand(int id) => Id = id;
    public int Id { get; private set; }
}
