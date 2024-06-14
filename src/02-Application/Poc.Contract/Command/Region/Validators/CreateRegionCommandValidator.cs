using FluentValidation;
using Poc.Contract.Command.Region.Request;

namespace Poc.Contract.Command.Region.Validators;

public class CreateRegionCommandValidator : AbstractValidator<CreateRegionCommand>
{
    public CreateRegionCommandValidator()
    {
        RuleFor(command => command.RegionName)
            .NotEmpty()
            .MaximumLength(50);

    }
}
