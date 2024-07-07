using FluentValidation;
using Poc.Contract.Query.Employee.Request;
namespace Poc.Contract.Query.Employee.Validators;

public class GetEmployeeByIdQueryValidator : AbstractValidator<GetEmployeeByIdQuery>
{
    public GetEmployeeByIdQueryValidator()
    {
        RuleFor(Command => Command.Id)
            .NotEmpty();
    }
}
