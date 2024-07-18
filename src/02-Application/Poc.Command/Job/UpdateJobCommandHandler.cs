using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Poc.Contract.Command.Job.Interfaces;
using Poc.Contract.Command.Job.Request;
using Poc.Contract.Command.Job.Validators;
using Poc.Domain.Entities.Job;

namespace Poc.Command.Job;

public class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand, Result>
{
    private readonly UpdateJobCommandValidator _validator;
    private readonly IJobWriteOnlyRepository _repo;
    private readonly ILogger<UpdateJobCommandHandler> _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UpdateJobCommandHandler(UpdateJobCommandValidator validator, IJobWriteOnlyRepository repo, ILogger<UpdateJobCommandHandler> logger, IMediator mediator, IMapper mapper)
    {
        _validator = validator;
        _repo = repo;
        _logger = logger;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<Result> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());

        var entity = await _repo.Get(request.JobId);
        if (entity == null)
            return Result.NotFound($"Nenhum registro encontrado pelo Id: {request.JobId}");

        entity = new JobEntity(request.JobId, request.JobTitle, request.MinSalary, request.MaxSalary);
        await _repo.Update(entity);

        foreach (var domainEvent in entity.DomainEvents)
            await _mediator.Publish(domainEvent);

        entity.ClearDomainEvents();

        return Result.SuccessWithMessage("Atualizado com sucesso!");
    }
}
