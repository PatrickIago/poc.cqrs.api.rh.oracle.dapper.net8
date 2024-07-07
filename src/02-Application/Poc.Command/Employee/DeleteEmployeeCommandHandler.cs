using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Poc.Contract.Command.Employee.Interfaces;
using Poc.Contract.Command.Employee.Request;
using Poc.Contract.Command.Employee.Validators;
using Poc.Domain.Entities.Employee;

namespace Poc.Command.Employee;
public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand,Result>
{
    private readonly DeleteEmployeeCommandValidator _validator;
    private readonly IEmployeeWriteOnlyRepository _repo;
    private readonly ILogger<DeleteEmployeeCommandHandler> _logger;
    private readonly IMediator _mediator;

    public DeleteEmployeeCommandHandler(DeleteEmployeeCommandValidator validator, IEmployeeWriteOnlyRepository repo, ILogger<DeleteEmployeeCommandHandler> logger, IMediator mediator)
    {
        _validator = validator;
        _repo = repo;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<Result> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());

        var entity = await _repo.Get(request.Id);
        if (entity == null)
            return Result.NotFound($"Nenhum registro encontrado pelo Id: {request.Id}");

        entity = new EmployeeEntity(request.Id);

        await _repo.Delete(entity.Id);

        foreach (var domainEvent in entity.DomainEvents)
            await _mediator.Publish(domainEvent);

        entity.ClearDomainEvents();

        return Result.SuccessWithMessage("Funcionario Removido com sucesso!");
    }
}
