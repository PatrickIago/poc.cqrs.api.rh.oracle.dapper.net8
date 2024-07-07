using FluentValidation;
using Poc.Contract.Command.Employee.Request;

namespace Poc.Contract.Command.Employee.Validators;
public class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
{
    public DeleteEmployeeCommandValidator()
    {
        RuleFor(Command => Command.Id)
            .NotEmpty();
    }
}
