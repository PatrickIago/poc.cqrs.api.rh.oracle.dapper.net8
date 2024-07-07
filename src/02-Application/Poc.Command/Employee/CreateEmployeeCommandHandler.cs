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

    // Método Handle que é executado quando o comando CreateEmployeeCommand é disparado
    public async Task<Result<CreateEmployeeResponse>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        // Valida o comando recebido
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors()); // Retorna erros de validação se houver

        // Cria uma nova entidade Employee com os dados do comando
        var entity = new EmployeeEntity(request.Name, request.Email, request.PhoneNumber, request.HireDate, request.JobId, request.Salary, request.ManagerId, request.Department);

        // Salva a entidade no repositório
        await _repo.Create(entity);

        // Publica eventos de domínio
        foreach (var domainEvent in entity.DomainEvents)
            await _mediator.Publish(domainEvent);

        // Limpa os eventos de domínio após a publicação
        entity.ClearDomainEvents();

        // Retorna um resultado de sucesso com a resposta do comando
        return Result.Success(new CreateEmployeeResponse(entity.Id), "Funcionario Cadastrado com sucesso!");
    }

    //trata a criação de um novo empregado, valida os dados, salva a entidade, publica eventos de domínio, e retorna uma resposta de sucesso.
}