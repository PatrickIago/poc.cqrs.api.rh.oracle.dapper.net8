using FluentValidation;
using Poc.Contract.Command.Employee.Request;

namespace Poc.Contract.Command.Employee.Validators;
public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator()
    {
        RuleFor(Command => Command.Id)
            .NotEmpty();
        RuleFor(Command => Command.Name).NotEmpty()
           .MaximumLength(50);
        RuleFor(Command => Command.Email).NotEmpty()
            .EmailAddress();
    }
}
