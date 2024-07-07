using Ardalis.Result;
using MediatR;
using Poc.Contract.Command.Employee.Response;
using Poc.Domain.Entities.Departament;
using System.ComponentModel.DataAnnotations;

namespace Poc.Contract.Command.Employee.Request;

public class CreateEmployeeCommand : IRequest<Result<CreateEmployeeResponse>>
{
    [Required]
    [MaxLength(100)]
    [DataType(DataType.Text)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [Phone]
    public string PhoneNumber { get; set; }

    [Required]
    public DateTime HireDate { get; set; }

    [Required]
    public int JobId { get; set; }

    [Required]
    public decimal Salary { get; set; }

    public int? ManagerId { get; set; } // ManagerId pode ser opcional

    [Required]
    public DepartmentEntity Department { get; set; }
}