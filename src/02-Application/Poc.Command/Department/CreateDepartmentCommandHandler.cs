using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Poc.Contract.Command.Departament.Interfaces;
using Poc.Contract.Command.Departament.Request;
using Poc.Contract.Command.Departament.Response;
using Poc.Contract.Command.Departament.Validators;

namespace Poc.Command.Departament;

// Define a classe para lidar com o comando de criação de departamento
public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, Result<CreateDepartmentResponse>>
{
    private readonly CreateDepartmentCommandValidator _validator;  // Validador para o comando de criação de departamento
    private readonly IDepartmentWriteOnlyRepository _repo;  // Repositório para operações de escrita de departamento
    private readonly ILogger<CreateDepartmentCommandHandler> _logger;  // Logger para registrar logs
    private readonly IMediator _mediator;  // Mediador para publicar eventos de domínio

    public CreateDepartmentCommandHandler(CreateDepartmentCommandValidator validator, IDepartmentWriteOnlyRepository repo, ILogger<CreateDepartmentCommandHandler> logger, IMediator mediator)
    {
        _validator = validator;
        _repo = repo;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<Result<CreateDepartmentResponse>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        // Valida o comando usando o validador injetado
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            // Retorna um resultado inválido se a validação falhar
            return Result.Invalid(validationResult.AsErrors());

        // Cria uma nova entidade de departamento com os dados da solicitação
        var entity = new DepartmentEntity(request.Name, request.ManagerId, request.Location);

        // Salva a nova entidade no repositório
        await _repo.Create(entity);

        // Publica eventos de domínio usando o mediador
        foreach (var domainEvent in entity.DomainEvents)
            await _mediator.Publish(domainEvent);

        // Limpa os eventos de domínio da entidade
        entity.ClearDomainEvents();

        // Retorna um resultado de sucesso com a resposta de criação do departamento
        return Result.Success(new CreateDepartmentResponse(entity.Id), "Departamento Cadastrado com sucesso!");
    }
}
// O CreateDepartamentCommandHandler valida e processa um comando para criar um departamento, incluindo a persistência dos dados e o envio de eventos relacionados ao domínio.