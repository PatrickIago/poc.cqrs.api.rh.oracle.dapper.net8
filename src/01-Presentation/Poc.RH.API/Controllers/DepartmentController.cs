using MediatR;
using Microsoft.AspNetCore.Mvc;
using Poc.Contract.Command.Departament.Request;
using Poc.Contract.Command.Departament.Response;
using Poc.Contract.Query.Departament.Request;
using Poc.Contract.Query.Departament.ViewModels;
using Poc.RH.API.Extensions;
using Poc.RH.API.Models;
using System.ComponentModel;
using System.Net.Mime;

namespace Poc.RH.API.Controllers;

/// <summary>
/// Controlador responsável por operações relacionadas a departamentos.
/// </summary>
[Route("api/v1/[controller]")]
[Produces("application/json")]
[ApiController]
[Description("Controller responsável por cadastrar departamentos.")]
[ApiExplorerSettings(GroupName = "Department")]
public class DepartmentController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<DepartmentController> _logger;

    /// <summary>
    /// Construtor do controlador de departamentos.
    /// </summary>
    /// <param name="logger">Serviço para log de operações e erros.</param>
    /// <param name="mediator">Mediador para operações CQRS.</param>
    public DepartmentController(ILogger<DepartmentController> logger, IMediator mediator)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Obtém uma lista com todos os departamentos.
    /// </summary>
    /// <response code="200">Retorna a lista de departamentos.</response>
    /// <response code="500">Quando ocorre um erro interno inesperado no servidor.</response>
    [HttpGet]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse<List<DepartmentQueryModel>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
        => (await _mediator.Send(new GetDepartmentQuery())).ToActionResult();

    /// <summary>
    /// Obtém o departamento pelo Id.
    /// </summary>
    /// <param name="id"></param>
    /// <response code="200">Retorna o departamento.</response>
    /// <response code="400">Retorna lista de erros, se a requisição for inválida.</response>
    /// <response code="404">Quando nenhum departamento é encontrado pelo Id fornecido.</response>
    /// <response code="500">Quando ocorre um erro interno inesperado no servidor.</response>
    [HttpGet("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse<DepartmentQueryModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(decimal id)
        => (await _mediator.Send(new GetDepartmentByIdQuery(id))).ToActionResult();

    /// <summary>
    /// Cadastra um novo departamento.
    /// </summary>
    /// <param name="command"></param>
    /// <response code="200">Retorna o Id do novo departamento.</response>
    /// <response code="400">Retorna lista de erros, se a requisição for inválida.</response>
    /// <response code="500">Quando ocorre um erro interno inesperado no servidor.</response>
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse<CreateDepartmentResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateDepartmentCommand command)
        => (await _mediator.Send(command)).ToActionResult();

    /// <summary>
    /// Atualiza um departamento existente.
    /// </summary>
    /// <param name="command"></param>
    /// <response code="200">Retorna a resposta com a mensagem de sucesso.</response>
    /// <response code="400">Retorna lista de erros, se a requisição for inválida.</response>
    /// <response code="404">Quando nenhum departamento é encontrado pelo Id fornecido.</response>
    /// <response code="500">Quando ocorre um erro interno inesperado no servidor.</response>
    [HttpPut]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromBody] UpdateDepartmentCommand command)
        => (await _mediator.Send(command)).ToActionResult();

    /// <summary>
    /// Deleta o departamento pelo Id.
    /// </summary>
    /// <param name="id"></param>
    /// <response code="200">Retorna a resposta com a mensagem de sucesso.</response>
    /// <response code="400">Retorna lista de erros, se a requisição for inválida.</response>
    /// <response code="404">Quando nenhum departamento é encontrado pelo Id fornecido.</response>
    /// <response code="500">Quando ocorre um erro interno inesperado no servidor.</response>
    [HttpDelete("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(decimal id)
        => (await _mediator.Send(new DeleteDepartmentCommand(id))).ToActionResult();
}