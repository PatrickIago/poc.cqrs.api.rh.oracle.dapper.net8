using FluentValidation;
using Poc.Contract.Query.Job.Request;

namespace Poc.Contract.Query.Job.Validators;
public class GetJobByIdQueryValidator : AbstractValidator<GetJobByIdQuery>
{
    public GetJobByIdQueryValidator()
    {
        RuleFor(command => command.JobId).NotEmpty();
    }
}
