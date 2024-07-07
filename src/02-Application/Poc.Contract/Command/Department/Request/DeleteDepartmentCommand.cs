using Ardalis.Result;
using MediatR;

namespace Poc.Contract.Command.Departament.Request; 
public class DeleteDepartmentCommand : IRequest<Result>
{
    public DeleteDepartmentCommand(decimal id) => Id = id;
    public decimal Id { get; private set; }
}
