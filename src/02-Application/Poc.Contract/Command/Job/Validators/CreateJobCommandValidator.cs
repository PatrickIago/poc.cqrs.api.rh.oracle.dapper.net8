using FluentValidation;
using Poc.Contract.Command.Job.Request;
namespace Poc.Contract.Command.Job.Validators;

public class CreateJobCommandValidator : AbstractValidator<CreateJobCommand>
{
    public CreateJobCommandValidator()
    {
        RuleFor(command => command.JobTitle).NotEmpty();
    }
}
