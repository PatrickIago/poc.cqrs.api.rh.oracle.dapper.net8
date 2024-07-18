using MediatR;
using Microsoft.AspNetCore.Mvc;
using Poc.Contract.Command.Job.Request;
using Poc.Contract.Command.Job.Response;
using Poc.Contract.Query.Job.Request;
using Poc.Contract.Query.Job.ViewModels;
using Poc.RH.API.Extensions;
using Poc.RH.API.Models;
using System.ComponentModel;
using System.Net.Mime;

namespace Poc.RH.API.Controllers;

/// <summary>
/// Controlador responsável por operações relacionadas a jobs.
/// </summary>
[Route("api/v1/[controller]")]
[Produces("application/json")]
[ApiController]
[Description("Controller responsável por cadastrar jobs.")]
[ApiExplorerSettings(GroupName = "Job")]
public class JobController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<JobController> _logger;

    /// <summary>
    /// Construtor do controlador de jobs.
    /// </summary>
    /// <param name="logger">Serviço para log de operações e erros.</param>
    /// <param name="mediator">Mediador para operações CQRS.</param>
    public JobController(ILogger<JobController> logger, IMediator mediator)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Obtém uma lista com todos os jobs.
    /// </summary>
    /// <response code="200">Retorna a lista de jobs.</response>
    /// <response code="500">Quando ocorre um erro interno inesperado no servidor.</response>
    [HttpGet]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse<List<JobQueryModel>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    //[Authorize(Roles = $"{RoleUserAuthConstants.Job}")]
    public async Task<IActionResult> GetAll()
        => (await _mediator.Send(new GetJobQuery())).ToActionResult();

    /// <summary>
    /// Obtém o job pelo Id.
    /// </summary>
    /// <param name="id"></param>
    /// <response code="200">Retorna o job.</response>
    /// <response code="400">Retorna lista de erros, se a requisição for inválida.</response>
    /// <response code="404">Quando nenhum job é encontrado pelo Id fornecido.</response>
    /// <response code="500">Quando ocorre um erro interno inesperado no servidor.</response>
    [HttpGet("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse<JobQueryModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    //[Authorize(Roles = $"{RoleUserAuthConstants.Job}")]
    public async Task<IActionResult> GetById(string id)
        => (await _mediator.Send(new GetJobByIdQuery(id))).ToActionResult();

    /// <summary>
    /// Cadastra um novo job.
    /// </summary>
    /// <param name="command"></param>
    /// <response code="200">Retorna o Id do novo job.</response>
    /// <response code="400">Retorna lista de erros, se a requisição for inválida.</response>
    /// <response code="500">Quando ocorre um erro interno inesperado no servidor.</response>
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse<CreateJobResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    //[Authorize(Roles = $"{RoleUserAuthConstants.Job}")]
    public async Task<IActionResult> Create([FromBody] CreateJobCommand command)
        => (await _mediator.Send(command)).ToActionResult();

    /// <summary>
    /// Atualiza um job existente.
    /// </summary>
    /// <param name="command"></param>
    /// <response code="200">Retorna a resposta com a mensagem de sucesso.</response>
    /// <response code="400">Retorna lista de erros, se a requisição for inválida.</response>
    /// <response code="404">Quando nenhum job é encontrado pelo Id fornecido.</response>
    /// <response code="500">Quando ocorre um erro interno inesperado no servidor.</response>
    [HttpPut]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    //[Authorize(Roles = $"{RoleUserAuthConstants.Job}")]
    public async Task<IActionResult> Update([FromBody] UpdateJobCommand command)
        => (await _mediator.Send(command)).ToActionResult();

    /// <summary>
    /// Deleta o job pelo Id.
    /// </summary>
    /// <param name="id"></param>
    /// <response code="200">Retorna a resposta com a mensagem de sucesso.</response>
    /// <response code="400">Retorna lista de erros, se a requisição for inválida.</response>
    /// <response code="404">Quando nenhum job é encontrado pelo Id fornecido.</response>
    /// <response code="500">Quando ocorre um erro interno inesperado no servidor.</response>
    [HttpDelete("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    //[Authorize(Roles = $"{RoleUserAuthConstants.Job}")]
    public async Task<IActionResult> Delete(string id)
        => (await _mediator.Send(new DeleteJobCommand(id))).ToActionResult();
}