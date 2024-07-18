using Ardalis.Result;
using MediatR;
using System.ComponentModel.DataAnnotations;
namespace Poc.Contract.Command.Job.Request;

public class UpdateJobCommand : IRequest<Result>
{
    [Required]
    public string JobId { get; set; }
    [Required]
    public string JobTitle { get; set; }
    public decimal MinSalary { get; set; }
    public decimal MaxSalary { get; set; }
}
