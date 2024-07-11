using FluentValidation;
using Poc.Contract.Command.Departament.Request;

namespace Poc.Contract.Command.Departament.Validators;
public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
{
    public CreateDepartmentCommandValidator()
    {
        RuleFor(Command => Command.Name)
            .NotEmpty()
            .MaximumLength(30);
    }
}
