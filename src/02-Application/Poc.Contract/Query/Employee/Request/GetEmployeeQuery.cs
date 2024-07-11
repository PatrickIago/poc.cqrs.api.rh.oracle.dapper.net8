using Ardalis.Result;
using MediatR;
using Poc.Contract.Query.Employee.ViewModels;
namespace Poc.Contract.Query.Employee.Request;
public class GetEmployeeQuery : IRequest<Result<List<EmployeeQueryModel>>>
{
    public GetEmployeeQuery()
    {
    }
}
