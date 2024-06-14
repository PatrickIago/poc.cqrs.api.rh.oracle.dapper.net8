using FluentValidation;
using Poc.Contract.Query.Region.Request;

namespace Poc.Contract.Query.Region.Validators;

public class GetRegionByIdQueryValidator : AbstractValidator<GetRegionByIdQuery>
{
    public GetRegionByIdQueryValidator()
    {
        RuleFor(command => command.RegionId).NotEmpty();
    }
}
