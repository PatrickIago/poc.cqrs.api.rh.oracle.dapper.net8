using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Poc.Contract.Command.Employee.Interfaces;
using Poc.Contract.Command.Employee.Request;
using Poc.Contract.Command.Employee.Validators;
using Poc.Domain.Entities.Employee;

namespace Poc.Command.Employee;
public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Result>
{
    private readonly UpdateEmployeeCommandValidator _validator;
    private readonly IEmployeeWriteOnlyRepository _repo;
    private readonly ILogger<UpdateEmployeeCommandHandler> _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UpdateEmployeeCommandHandler(UpdateEmployeeCommandValidator validator, IEmployeeWriteOnlyRepository repo, ILogger<UpdateEmployeeCommandHandler> logger, IMediator mediator, IMapper mapper)
    {
        _validator = validator;
        _repo = repo;
        _logger = logger;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<Result> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());

        var entity = await _repo.Get(request.EmployeeId);
        if (entity == null)
            return Result.NotFound($"Nenhum registro encontrado pelo Id:{request.EmployeeId}");

        entity = new EmployeeEntity(request.EmployeeId, request.FirstName, request.LastName, request.Email, request.Phone, request.HireDate,request.JobId, request.Salary, request.CommissionPct, request.ManagerId, request.DepartmentId);
        await _repo.Update(entity);

        foreach (var domainEvent in entity.DomainEvents)
            await _mediator.Publish(domainEvent);

        entity.ClearDomainEvents();

        return Result.SuccessWithMessage("Atualizado com sucesso!");
    }
}
