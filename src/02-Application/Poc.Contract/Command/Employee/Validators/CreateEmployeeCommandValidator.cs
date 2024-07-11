using FluentValidation;
using Poc.Contract.Command.Employee.Request;

namespace Poc.Contract.Command.Employee.Validators;
public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(command => command.FirstName)
            .NotEmpty()
            .MaximumLength(50);
        RuleFor(command => command.LastName)
            .NotEmpty()
            .MaximumLength(50);
    }
}
