using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Poc.Contract.Command.Job.Interfaces;
using Poc.Contract.Command.Job.Request;
using Poc.Contract.Command.Job.Response;
using Poc.Contract.Command.Job.Validators;
using Poc.Domain.Entities.Job;

namespace Poc.Command.Job;
public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, Result<CreateJobResponse>>
{
    private readonly CreateJobCommandValidator _validator;
    private readonly IJobWriteOnlyRepository _repo;
    private readonly ILogger<CreateJobCommandHandler> _logger;
    private readonly IMediator _mediator;

    public CreateJobCommandHandler(CreateJobCommandValidator validator, IJobWriteOnlyRepository repo, ILogger<CreateJobCommandHandler> logger, IMediator mediator)
    {
        _validator = validator;
        _repo = repo;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<Result<CreateJobResponse>> Handle(CreateJobCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());

        var entity = new JobEntity(request.JobId, request.JobTitle, request.MinSalary, request.MaxSalary);
        await _repo.Create(entity);

        foreach (var domainEvents in entity.DomainEvents)
            await _mediator.Publish(domainEvents);

        return Result.Success(new CreateJobResponse(entity.JobId), "Cadastrado com sucesso!");
    }
}
