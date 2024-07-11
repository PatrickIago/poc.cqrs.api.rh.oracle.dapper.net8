using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Poc.Contract.Command.Departament.Interfaces;
using Poc.Contract.Command.Departament.Request;
using Poc.Contract.Command.Departament.Validators;

namespace Poc.Command.Departament;
public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, Result>
{
    private readonly DeleteDepartmentCommandValidator _validator;
    private readonly IDepartmentWriteOnlyRepository _repo;
    private readonly ILogger<DeleteDepartmentCommandHandler> _logger;
    private readonly IMediator _mediator;

    public DeleteDepartmentCommandHandler(DeleteDepartmentCommandValidator validator, IDepartmentWriteOnlyRepository repo, ILogger<DeleteDepartmentCommandHandler> logger, IMediator mediator)
    {
        _validator = validator;
        _repo = repo;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<Result> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());

        var entity = await _repo.Get(request.Id);
        if (entity == null)
            return Result.NotFound($"Nenhum registro encontrado pelo Id: {request.Id}");

        entity = new DepartmentEntity(request.Id);

        await _repo.Delete(entity.Id);

        foreach (var domainEvent in entity.DomainEvents)
            await _mediator.Publish(domainEvent);

        entity.ClearDomainEvents();

        return Result.SuccessWithMessage("Departamento Removido com sucesso!");

    }
}
