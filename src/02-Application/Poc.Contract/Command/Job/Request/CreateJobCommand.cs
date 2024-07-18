using Ardalis.Result;
using MediatR;
using Poc.Contract.Command.Job.Response;
using System.ComponentModel.DataAnnotations;

namespace Poc.Contract.Command.Job.Request;
public class CreateJobCommand : IRequest<Result<CreateJobResponse>>
{
    [Required]
    public string JobId { get; set; }
    public string JobTitle { get; set; }
    public decimal MinSalary { get; set; }
    public decimal MaxSalary { get; set; }
}
