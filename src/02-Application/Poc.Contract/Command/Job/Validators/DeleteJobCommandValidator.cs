using FluentValidation;
using Poc.Contract.Command.Job.Request;
namespace Poc.Contract.Command.Job.Validators;

public class DeleteJobCommandValidator : AbstractValidator<DeleteJobCommand>
{
    public DeleteJobCommandValidator()
    {
        RuleFor(command => command.JobId).NotEmpty();
    }
}
