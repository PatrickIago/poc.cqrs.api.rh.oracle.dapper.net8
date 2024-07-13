using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Poc.Contract.Command.JobHistory.Interfaces;
using Poc.Contract.Command.JobHistory.Request;
using Poc.Contract.Command.JobHistory.Response;
using Poc.Contract.Command.JobHistory.Validators;
using Poc.Domain.Entities.JobHistory;

namespace Poc.Command.JobHistory;
public class CreateJobHistoryCommandHandler : IRequestHandler<CreateJobHistoryCommand, Result<CreateJobHistoryResponse>>
{
    private readonly CreateJobHistoryCommandValidator _validator;
    private readonly IJobHistoryWriteOnlyRepository _repo;
    private readonly ILogger<CreateJobHistoryCommandHandler> _logger;
    private readonly IMediator _mediator;

    public CreateJobHistoryCommandHandler(CreateJobHistoryCommandValidator validator, IJobHistoryWriteOnlyRepository repo, ILogger<CreateJobHistoryCommandHandler> logger, IMediator mediator)
    {
        _validator = validator;
        _repo = repo;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<Result<CreateJobHistoryResponse>> Handle(CreateJobHistoryCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());

        var entity = new JobHistoryEntity(request.EmployeeId, request.StartDate, request.EndDate, request.JobId, request.DepartmentId);

        await _repo.Create(entity);

        foreach (var domainEvent in entity.DomainEvents)
            await _mediator.Publish(domainEvent);

        entity.ClearDomainEvents();

        return Result.Success(new CreateJobHistoryResponse(entity.EmployeeId), "Cadastrado com sucesso");
    }
}
