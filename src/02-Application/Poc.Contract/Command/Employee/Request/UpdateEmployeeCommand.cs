using Ardalis.Result;
using MediatR;
using Poc.Domain.Entities.Departament;
using System.ComponentModel.DataAnnotations;

namespace Poc.Contract.Command.Employee.Request;
public class UpdateEmployeeCommand : IRequest<Result>
{
    [Required]
    public int Id { get; set; }

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
    [Range(0, double.MaxValue)]
    public decimal Salary { get; set; }

    public int? ManagerId { get; set; } 

    [Required]
    public DepartmentEntity Department { get; set; }
}
