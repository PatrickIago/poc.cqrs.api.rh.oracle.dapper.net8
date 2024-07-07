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
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateEmployeeCommandHandler> _logger;
    private readonly IMediator _mediator;

    public UpdateEmployeeCommandHandler(UpdateEmployeeCommandValidator validator, IEmployeeWriteOnlyRepository repo, IMapper mapper, ILogger<UpdateEmployeeCommandHandler> logger, IMediator mediator)
    {
        _validator = validator;
        _repo = repo;
        _mapper = mapper;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<Result> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
       var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());

        var entity = new EmployeeEntity(request.Id,request.Name, request.Email, request.PhoneNumber, request.HireDate, request.JobId, request.Salary, request.ManagerId, request.Department);
        await _repo.Get(request.Id);

        await _repo.Update(entity);

        foreach (var domainEvent in entity.DomainEvents)
            await _mediator.Publish(domainEvent);

        entity.ClearDomainEvents();

        return Result.SuccessWithMessage("Funcionario atualizado com sucesso!");

    }
}
