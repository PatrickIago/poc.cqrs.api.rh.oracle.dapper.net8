using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Poc.Contract.Command.Region.Interfaces;
using Poc.Contract.Command.Region.Request;
using Poc.Contract.Command.Region.Validators;
using Poc.Domain.Entities.Region;

namespace Poc.Command.Region;

public class DeleteRegionCommandHandler : IRequestHandler<DeleteRegionCommand, Result>
{
    private readonly DeleteRegionCommandValidator _validator;
    private readonly IRegionWriteOnlyRepository _repo;
    private readonly ILogger<DeleteRegionCommandHandler> _logger;
    private readonly IMediator _mediator;
    public DeleteRegionCommandHandler(ILogger<DeleteRegionCommandHandler> logger,
                                    IRegionWriteOnlyRepository repo,
                                    DeleteRegionCommandValidator validator,
                                    IMediator mediator)
    {
        _repo = repo;
        _logger = logger;
        _validator = validator;
        _mediator = mediator;
    }

    public async Task<Result> Handle(DeleteRegionCommand request, CancellationToken cancellationToken)
    {
        // Validanto a requisição.
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());

        // Obtendo o registro da base.
        var entity = await _repo.Get(request.RegionId);
        if (entity == null)
            return Result.NotFound($"Nenhum registro encontrado pelo Id: {request.RegionId}");

        entity = new RegionEntity(request.RegionId);

        await _repo.Delete(entity.RegionId, request.UserIdDelete);

        // Executa eventos
        foreach (var domainEvent in entity.DomainEvents)
            await _mediator.Publish(domainEvent);

        entity.ClearDomainEvents();

        return Result.SuccessWithMessage("Removido com sucesso!");
    }
}