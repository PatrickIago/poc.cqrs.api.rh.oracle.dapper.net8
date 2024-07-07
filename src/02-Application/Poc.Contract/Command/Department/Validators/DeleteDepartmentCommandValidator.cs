using FluentValidation;
using Poc.Contract.Command.Departament.Request;

namespace Poc.Contract.Command.Departament.Validators;
public class DeleteDepartmentCommandValidator : AbstractValidator<DeleteDepartmentCommand>
{
    public DeleteDepartmentCommandValidator()
    {
        RuleFor(Command => Command.Id)
             .NotEmpty();
    }
}
