using Ardalis.Result;
using MediatR;
using Poc.Contract.Command.Employee.Response;
using System.ComponentModel.DataAnnotations;

namespace Poc.Contract.Command.Employee.Request;
public class CreateEmployeeCommand : IRequest<Result<CreateEmployeeResponse>>
{
    [Required]
    [MaxLength(50)]
    [DataType(DataType.Text)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(50)]
    [DataType(DataType.Text)]
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime HireDate { get; set; }
    public string JobId { get; set; }
    public decimal Salary { get; set; }
    public decimal CommissionPct { get; set; }
    public int ManagerId { get; set; }
    public int DepartmentId { get; set; }
}
