using FluentValidation;
using Poc.Contract.Query.Departament.Request;

namespace Poc.Contract.Query.Departament.Validators;
public class GetDepartmentByIdQueryValidator : AbstractValidator<GetDepartmentByIdQuery>
{
    public GetDepartmentByIdQueryValidator()
    {
        RuleFor(Command => Command.Id).NotEmpty();
    }
}
