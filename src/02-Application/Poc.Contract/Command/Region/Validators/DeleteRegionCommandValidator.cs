using FluentValidation;
using Poc.Contract.Command.Region.Request;

namespace Poc.Contract.Command.Region.Validators;

public class DeleteRegionCommandValidator : AbstractValidator<DeleteRegionCommand>
{
    public DeleteRegionCommandValidator()
    {
        RuleFor(command => command.RegionId)
            .NotEmpty();
    }
}
