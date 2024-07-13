using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Poc.Contract.Command.JobHistory.Interfaces;
using Poc.Contract.Command.JobHistory.Request;
using Poc.Contract.Command.JobHistory.Validators;
using Poc.Domain.Entities.JobHistory;

namespace Poc.Command.JobHistory;
public class DeleteJobHistoryCommandHandler : IRequestHandler<DeleteJobHistoryCommand, Result>
{
    private readonly DeleteJobHistoyCommandValidator _validator;
    private readonly IJobHistoryWriteOnlyRepository _repo;
    private readonly ILogger<DeleteJobHistoryCommandHandler> _logger;
    private readonly IMediator _mediator;

    public DeleteJobHistoryCommandHandler(DeleteJobHistoyCommandValidator validator, IJobHistoryWriteOnlyRepository repo, ILogger<DeleteJobHistoryCommandHandler> logger, IMediator mediator)
    {
        _validator = validator;
        _repo = repo;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<Result> Handle(DeleteJobHistoryCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());

        // Obtendo o registro da base.
        var entity = await _repo.Get(request.EmployeeId);
        if (entity == null)
            return Result.NotFound($"Nenhum registro encontrado pelo Id: {request.EmployeeId}");

        entity = new JobHistoryEntity(request.EmployeeId);

        await _repo.Delete(entity.EmployeeId);

        // Executa eventos
        foreach (var domainEvent in entity.DomainEvents)
            await _mediator.Publish(domainEvent);

        entity.ClearDomainEvents();

        return Result.SuccessWithMessage("Removido com sucesso!");
    }
}
