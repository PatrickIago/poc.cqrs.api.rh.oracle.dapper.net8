using FluentValidation;
using Poc.Contract.Command.JobHistory.Request;

namespace Poc.Contract.Command.JobHistory.Validators;
public class UpdateJobHistoryCommandValidator : AbstractValidator<UpdateJobHistoryCommand>
{
    public UpdateJobHistoryCommandValidator()
    {
        RuleFor(command => command.EmployeeId).NotEmpty();
    }
}

