using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Poc.Contract.Command.Employee.Interfaces;
using Poc.Contract.Command.Employee.Request;
using Poc.Contract.Command.Employee.Response;
using Poc.Contract.Command.Employee.Validators;
using Poc.Domain.Entities.Employee;

namespace Poc.Command.Employee;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Result<CreateEmployeeResponse>>
{
    private readonly CreateEmployeeCommandValidator _validator;
    private readonly IEmployeeWriteOnlyRepository _repo;
    private readonly ILogger<CreateEmployeeCommandHandler> _logger;
    private readonly IMediator _mediator;

    public CreateEmployeeCommandHandler(CreateEmployeeCommandValidator validator, IEmployeeWriteOnlyRepository repo, ILogger<CreateEmployeeCommandHandler> logger, IMediator mediator)
    {
        _validator = validator;
        _repo = repo;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<Result<CreateEmployeeResponse>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());

        var entity = new EmployeeEntity(request.FirstName, request.LastName, request.Email, request.Phone, request.HireDate,request.JobId, request.Salary, request.CommissionPct, request.ManagerId, request.DepartmentId);
        await _repo.Create(entity);

        foreach (var domainEvents in entity.DomainEvents)
            await _mediator.Publish(domainEvents);

        return Result.Success(new CreateEmployeeResponse(entity.EmployeeId), "Cadastrado com sucesso!");

    }
}
