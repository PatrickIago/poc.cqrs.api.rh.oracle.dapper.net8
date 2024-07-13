using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Poc.Contract.Command.Employee.Interfaces;
using Poc.Contract.Command.Employee.Request;
using Poc.Contract.Command.Employee.Validators;
using Poc.Contract.Command.JobHistory.Interfaces;
public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Result>
{
    private readonly DeleteEmployeeCommandValidator _validator;
    private readonly IEmployeeWriteOnlyRepository _repo;
    private readonly IJobHistoryWriteOnlyRepository _jobHistoryRepo;
    private readonly ILogger<DeleteEmployeeCommandHandler> _logger;
    private readonly IMediator _mediator;

    public DeleteEmployeeCommandHandler(DeleteEmployeeCommandValidator validator, IEmployeeWriteOnlyRepository repo, IJobHistoryWriteOnlyRepository jobHistoryRepo, ILogger<DeleteEmployeeCommandHandler> logger, IMediator mediator)
    {
        _validator = validator;
        _repo = repo;
        _jobHistoryRepo = jobHistoryRepo;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<Result> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());

        var employee = await _repo.Get(request.EmployeeId);
        if (employee == null)
            return Result.NotFound($"Nenhum registro encontrado pelo Id: {request.EmployeeId}");

        var jobHistories = await _jobHistoryRepo.GetByEmployeeId(request.EmployeeId);
        if (jobHistories != null && jobHistories.Any())
        {
            foreach (var jobHistory in jobHistories)
            {
                await _jobHistoryRepo.Delete(jobHistory.EmployeeId);
            }
        }

        await _repo.Delete(employee.EmployeeId);

        foreach (var domainEvent in employee.DomainEvents)
            await _mediator.Publish(domainEvent);

        employee.ClearDomainEvents();

        return Result.SuccessWithMessage("Removido com sucesso!");
    }
}