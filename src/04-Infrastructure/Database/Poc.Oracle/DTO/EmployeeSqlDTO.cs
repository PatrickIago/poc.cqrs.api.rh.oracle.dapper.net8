using Poc.Domain.Entities.Departament;
using Poc.Domain.Entities.Employee;
namespace Poc.Oracle.DTO;
public class EmployeeSqlDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime HireDate { get; set; }
    public int JobId { get; set; }
    public decimal Salary { get; set; }
    public int ManagerId { get; set; }
    public DepartamentEntity Department { get; set; }

    public EmployeeEntity MapToResult()
    {
        return new EmployeeEntity(Id, Name,  Email,  PhoneNumber,  HireDate,  JobId,  Salary,  ManagerId,  Department);
    }
}