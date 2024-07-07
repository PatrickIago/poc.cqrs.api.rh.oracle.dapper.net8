using Ardalis.Result;
using MediatR;
using Poc.Contract.Query.Departament.ViewModels;

namespace Poc.Contract.Query.Departament.Request;
public class GetDepartmentByIdQuery : IRequest<Result<DepartmentQueryModel>>
{
    public GetDepartmentByIdQuery(decimal id)
    {
        Id = id;
    }

    public decimal Id { get; set; }
}
