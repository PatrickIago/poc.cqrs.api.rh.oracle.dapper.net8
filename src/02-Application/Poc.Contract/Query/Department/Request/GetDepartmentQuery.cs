using Ardalis.Result;
using MediatR;
using Poc.Contract.Query.Departament.ViewModels;

namespace Poc.Contract.Query.Departament.Request;
public class GetDepartmentQuery : IRequest<Result<List<DepartmentQueryModel>>>
{
    public GetDepartmentQuery()
    {
    }
}
