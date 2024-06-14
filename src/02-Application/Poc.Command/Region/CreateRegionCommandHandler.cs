using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Poc.Contract.Command.Region.Interfaces;
using Poc.Contract.Command.Region.Request;
using Poc.Contract.Command.Region.Response;
using Poc.Contract.Command.Region.Validators;
using Poc.Domain.Entities.Region;

namespace Poc.Command.Region;

public class CreateRegionCommandHandler : IRequestHandler<CreateRegionCommand, Result<CreateRegionResponse>>
{
    private readonly CreateRegionCommandValidator _validator;
    private readonly IRegionWriteOnlyRepository _repo;
    private readonly ILogger<CreateRegionCommandHandler> _logger;
    private readonly IMediator _mediator;
    public CreateRegionCommandHandler(ILogger<CreateRegionCommandHandler> logger,
                                    IRegionWriteOnlyRepository repo,
                                    IMediator mediator,
                                    CreateRegionCommandValidator validator)
    {
        _repo = repo;
        _logger = logger;
        _validator = validator;
        _mediator = mediator;
    }
    public async Task<Result<CreateRegionResponse>> Handle(CreateRegionCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());

        var entity = new RegionEntity(request.RegionName);

        await _repo.Create(entity);

        // Executa eventos
        foreach (var domainEvent in entity.DomainEvents)
            await _mediator.Publish(domainEvent);

        entity.ClearDomainEvents();

        return Result.Success(new CreateRegionResponse(entity.RegionId), "Cadastrado com sucesso!");
    }
}
