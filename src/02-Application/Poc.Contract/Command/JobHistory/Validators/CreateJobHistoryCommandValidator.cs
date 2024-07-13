using FluentValidation;
using Poc.Contract.Command.JobHistory.Request;

namespace Poc.Contract.Command.JobHistory.Validators;
public class CreateJobHistoryCommandValidator : AbstractValidator<CreateJobHistoryCommand>
{
    public CreateJobHistoryCommandValidator()
    {
        RuleFor(command => command.EmployeeId).NotEmpty();
    }
}
