using Amazon.Runtime.Internal.Util;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Poc.Contract.Command.JobHistory.Interfaces;
using Poc.Contract.Command.JobHistory.Request;
using Poc.Contract.Command.JobHistory.Validators;
using Poc.Domain.Entities.JobHistory;

namespace Poc.Command.JobHistory;

public class UpdateJobHistoyCommandHandler : IRequestHandler<UpdateJobHistoryCommand, Result>
{
    private readonly UpdateJobHistoryCommandValidator _validator;
    private readonly IJobHistoryWriteOnlyRepository _repo;
    private readonly ILogger<UpdateJobHistoyCommandHandler> _logger;
    private readonly IMediator _mediator;

    public UpdateJobHistoyCommandHandler(UpdateJobHistoryCommandValidator validator, IJobHistoryWriteOnlyRepository repo, ILogger<UpdateJobHistoyCommandHandler> logger, IMediator mediator)
    {
        _validator = validator;
        _repo = repo;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<Result> Handle(UpdateJobHistoryCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());

        var entity = await _repo.Get(request.EmployeeId);
        if(entity == null)
            return Result.NotFound($"Nenhum registro econtrado pelo Id:{request.EmployeeId}");

        entity = new JobHistoryEntity(request.EmployeeId,request.StartDate,request.EndDate,request.JobId,request.DepartmentId);
        await _repo.Update(entity);

        foreach (var domainEvent in entity.DomainEvents)
            await _mediator.Publish(domainEvent);

        entity.ClearDomainEvents();

        return Result.SuccessWithMessage("Atualizado com sucesso!");
    }
}
