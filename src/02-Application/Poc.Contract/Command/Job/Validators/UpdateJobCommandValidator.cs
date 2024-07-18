using FluentValidation;
using Poc.Contract.Command.Job.Request;

namespace Poc.Contract.Command.Job.Validators;
public class UpdateJobCommandValidator : AbstractValidator<UpdateJobCommand>
{
    public UpdateJobCommandValidator()
    {
        RuleFor(command => command.JobId).NotEmpty();
        RuleFor(command => command.JobTitle).NotEmpty();
    }
}
