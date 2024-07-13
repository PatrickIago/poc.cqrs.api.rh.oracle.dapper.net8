using FluentValidation;
using Poc.Contract.Command.JobHistory.Request;
namespace Poc.Contract.Command.JobHistory.Validators;

public class DeleteJobHistoyCommandValidator : AbstractValidator<DeleteJobHistoryCommand>
{
    public DeleteJobHistoyCommandValidator()
    {
        RuleFor(command => command.EmployeeId).NotEmpty();
    }
}
