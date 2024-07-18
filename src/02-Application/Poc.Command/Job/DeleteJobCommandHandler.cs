using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Poc.Contract.Command.Job.Interfaces;
using Poc.Contract.Command.Job.Request;
using Poc.Contract.Command.Job.Validators;
namespace Poc.Command.Job;
public class DeleteJobCommandHandler : IRequestHandler<DeleteJobCommand, Result>
{
    private readonly IJobWriteOnlyRepository _repo;
    private readonly ILogger<DeleteJobCommandHandler> _logger;
    private readonly DeleteJobCommandValidator _validator;
    private readonly IMediator _mediator;

    public DeleteJobCommandHandler(IJobWriteOnlyRepository repo, ILogger<DeleteJobCommandHandler> logger, DeleteJobCommandValidator validator, IMediator mediator)
    {
        _repo = repo;
        _logger = logger;
        _validator = validator;
        _mediator = mediator;
    }

    public async Task<Result> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());

        var entity = await _repo.Get(request.JobId);
        if (entity == null)
            return Result.NotFound($"Nenhum registro encontrado pelo Id: {request.JobId}");

        await _repo.Delete(entity.JobId);

        foreach (var domainEvent in entity.DomainEvents)
            await _mediator.Publish(domainEvent);

        entity.ClearDomainEvents();

        return Result.SuccessWithMessage("Removido com sucesso!");
    }
}
