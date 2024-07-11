using FluentValidation;
using Poc.Contract.Command.Employee.Request;
namespace Poc.Contract.Command.Employee.Validators;
public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator()
    {
        RuleFor(command => command.EmployeeId)
            .NotEmpty();
        RuleFor(command => command.FirstName)
         .NotEmpty()
         .MaximumLength(50);
        RuleFor(command => command.LastName)
            .NotEmpty()
            .MaximumLength(50);
    }
}
