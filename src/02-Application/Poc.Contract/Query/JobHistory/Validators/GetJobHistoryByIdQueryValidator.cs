using FluentValidation;
using Poc.Contract.Query.JobHistory.Request;
namespace Poc.Contract.Query.JobHistory.Validators;

public class GetJobHistoryByIdQueryValidator : AbstractValidator<GetJobHistoryByIdQuery>
{
    public GetJobHistoryByIdQueryValidator()
    {
        RuleFor(command => command.EmployeeId).NotEmpty();
    }
}
