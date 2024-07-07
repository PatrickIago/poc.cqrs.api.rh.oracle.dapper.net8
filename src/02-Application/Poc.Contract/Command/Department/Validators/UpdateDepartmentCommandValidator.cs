using FluentValidation;
using Poc.Contract.Command.Departament.Request;
namespace Poc.Contract.Command.Departament.Validators;

public class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
{
    public UpdateDepartmentCommandValidator()
    {
        RuleFor(Command => Command.Id)
            .NotEmpty();

        RuleFor(Command => Command.Name)
            .NotEmpty()
            .MaximumLength(30);
    }
}
