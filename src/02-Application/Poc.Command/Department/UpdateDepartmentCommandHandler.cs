using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Poc.Contract.Command.Departament.Interfaces;
using Poc.Contract.Command.Departament.Request;
using Poc.Contract.Command.Departament.Validators;
using Poc.Domain.Entities.Departament;

namespace Poc.Command.Departament;

public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, Result>
{
    private readonly UpdateDepartmentCommandValidator _validator;
    private readonly IDepartmentWriteOnlyRepository _repo;
    private readonly ILogger<UpdateDepartmentCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public UpdateDepartmentCommandHandler(UpdateDepartmentCommandValidator validator, IDepartmentWriteOnlyRepository repo, ILogger<UpdateDepartmentCommandHandler> logger, IMapper mapper, IMediator mediator)
    {
        _validator = validator;
        _repo = repo;
        _logger = logger;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<Result> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        // Validando requisição
        var validationResult = await _validator.ValidateAsync(request,cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());

        // Obtendo o registro da base.
        var entity = await _repo.Get(request.Id);
        if (entity == null)
            return Result.NotFound($"Nenhum registro encontrado pelo Id: {request.Id}");

        entity = new DepartmentEntity(request.Id, request.Name,request.ManagerId,request.Location);
        await _repo.Update(entity);

        // Executa eventos
        foreach (var domainEvent in entity.DomainEvents)
            await _mediator.Publish(domainEvent);

        entity.ClearDomainEvents();

        return Result.SuccessWithMessage("Atualizado com sucesso!");
    }
}
