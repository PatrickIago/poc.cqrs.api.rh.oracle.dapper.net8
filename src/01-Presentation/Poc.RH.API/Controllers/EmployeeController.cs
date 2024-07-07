using MediatR;
using Microsoft.AspNetCore.Mvc;
using poc.core.api.net8.User;
using Poc.Contract.Command.Employee.Request;
using Poc.Contract.Command.Employee.Response;
using Poc.Contract.Query.Employee.Request;
using Poc.Contract.Query.Employee.ViewModels;
using Poc.RH.API.Extensions;
using Poc.RH.API.Models;
using Refit;
using System.ComponentModel;
using System.Net.Mime;

namespace Poc.RH.API.Controllers;

/// <summary>
/// Controlador responsável por operações relacionadas a empregados.
/// </summary>
[Route("api/v1/[controller]")]
[Produces("application/json")]
[ApiController]
[Description("Controller responsável por cadastrar empregados.")]
[ApiExplorerSettings(GroupName = "Employee")]
public class EmployeeController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<EmployeeController> _logger;

    /// <summary>
    /// Construtor do controlador de empregados.
    /// </summary>
    /// <param name="logger">Serviço para log de operações e erros.</param>
    /// <param name="mediator">Mediador para operações CQRS.</param>
    public EmployeeController(ILogger<EmployeeController> logger, IMediator mediator)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Obtém uma lista com todos os empregados.
    /// </summary>
    /// <response code="200">Retorna a lista de empregados.</response>
    /// <response code="500">Quando ocorre um erro interno inesperado no servidor.</response>
    [HttpGet]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Models.ApiResponse<List<EmployeeQueryModel>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    //[Authorize(Roles = $"{RoleUserAuthConstants.Employee}")]
    public async Task<IActionResult> GetAll()
        => (await _mediator.Send(new GetEmployeeQuery())).ToActionResult();

    /// <summary>
    /// Obtém o empregado pelo Id.
    /// </summary>
    /// <param name="id"></param>
    /// <response code="200">Retorna o empregado.</response>
    /// <response code="400">Retorna lista de erros, se a requisição for inválida.</response>
    /// <response code="404">Quando nenhum empregado é encontrado pelo Id fornecido.</response>
    /// <response code="500">Quando ocorre um erro interno inesperado no servidor.</response>
    [HttpGet("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Models.ApiResponse<EmployeeQueryModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    //[Authorize(Roles = $"{RoleUserAuthConstants.Employee}")]
    public async Task<IActionResult> GetById(int id)
        => (await _mediator.Send(new GetEmployeeByIdQuery(id))).ToActionResult();

    /// <summary>
    /// Cadastra um novo empregado.
    /// </summary>
    /// <param name="command"></param>
    /// <response code="200">Retorna o Id do novo empregado.</response>
    /// <response code="400">Retorna lista de erros, se a requisição for inválida.</response>
    /// <response code="500">Quando ocorre um erro interno inesperado no servidor.</response>
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Models.ApiResponse<CreateEmployeeResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    //[Authorize(Roles = $"{RoleUserAuthConstants.Employee}")]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeCommand command)
        => (await _mediator.Send(command)).ToActionResult();

    /// <summary>
    /// Atualiza um empregado existente.
    /// </summary>
    /// <param name="command"></param>
    /// <response code="200">Retorna a resposta com a mensagem de sucesso.</response>
    /// <response code="400">Retorna lista de erros, se a requisição for inválida.</response>
    /// <response code="404">Quando nenhum empregado é encontrado pelo Id fornecido.</response>
    /// <response code="500">Quando ocorre um erro interno inesperado no servidor.</response>
    [HttpPut]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    //[Authorize(Roles = $"{RoleUserAuthConstants.Employee}")]
    public async Task<IActionResult> Update([FromBody] UpdateEmployeeCommand command)
        => (await _mediator.Send(command)).ToActionResult();

    /// <summary>
    /// Deleta o empregado pelo Id.
    /// </summary>
    /// <param name="id"></param>
    /// <response code="200">Retorna a resposta com a mensagem de sucesso.</response>
    /// <response code="400">Retorna lista de erros, se a requisição for inválida.</response>
    /// <response code="404">Quando nenhum empregado é encontrado pelo Id fornecido.</response>
    /// <response code="500">Quando ocorre um erro interno inesperado no servidor.</response>
    [HttpDelete("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    //[Authorize(Roles = $"{RoleUserAuthConstants.Employee}")]
    public async Task<IActionResult> Delete(int id)
        => (await _mediator.Send(new DeleteEmployeeCommand(id))).ToActionResult();
}