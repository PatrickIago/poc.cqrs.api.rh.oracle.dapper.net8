using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Poc.Contract.Command.Region.Interfaces;
using Poc.Contract.Command.Region.Request;
using Poc.Contract.Command.Region.Validators;
using Poc.Domain.Entities.Region;

namespace Poc.Command.Region;

public class UpdateRegionCommandHandler : IRequestHandler<UpdateRegionCommand, Result>
{
    private readonly UpdateRegionCommandValidator _validator;
    private readonly IRegionWriteOnlyRepository _repo;
    private readonly ILogger<UpdateRegionCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    public UpdateRegionCommandHandler(ILogger<UpdateRegionCommandHandler> logger,
                                    IRegionWriteOnlyRepository repo,
                                    UpdateRegionCommandValidator validator,
                                    IMapper mapper,
                                    IMediator mediator)
    {
        _repo = repo;
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _mediator = mediator;
    }
    public async Task<Result> Handle(UpdateRegionCommand request, CancellationToken cancellationToken)
    {
        // Validanto a requisição.
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());

        // Obtendo o registro da base.
        var entity = await _repo.Get(request.RegionId);
        if (entity == null)
            return Result.NotFound($"Nenhum registro encontrado pelo Id: {request.RegionId}");

        entity = new RegionEntity(request.RegionId, request.RegionName);

        await _repo.Update(entity);

        // Executa eventos
        foreach (var domainEvent in entity.DomainEvents)
            await _mediator.Publish(domainEvent);

        entity.ClearDomainEvents();

        return Result.SuccessWithMessage("Atualizado com sucesso!");
    }
}