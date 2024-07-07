using Ardalis.Result;
using MediatR;
using Poc.Contract.Command.Departament.Response;
using System.ComponentModel.DataAnnotations;

namespace Poc.Contract.Command.Departament.Request;

public class CreateDepartmentCommand : IRequest<Result<CreateDepartmentResponse>>
{
    [Required]
    [MaxLength(100)]
    [DataType(DataType.Text)]
    public string Name { get; set; }

    [Required]
    public decimal ManagerId { get; set; }

    [Required]
    public decimal Location { get; set; }
}