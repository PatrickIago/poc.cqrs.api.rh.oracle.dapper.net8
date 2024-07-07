using FluentValidation;
using Poc.Contract.Command.Employee.Request;

namespace Poc.Contract.Command.Employee.Validators;
public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand> // Valida como criar um funcionario.
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(Command => Command.Name).NotEmpty()
            .MaximumLength(50);
        RuleFor(Command => Command.Email).NotEmpty()
            .EmailAddress();
    }
}
