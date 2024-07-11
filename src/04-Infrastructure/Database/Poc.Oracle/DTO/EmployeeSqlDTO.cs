using Poc.Domain.Entities.Employee;
namespace Poc.Oracle.DTO;
public class EmployeeSqlDTO
{
    public decimal EmployeeId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime HireDate { get; set; }
    public string JobId { get; set; }
    public decimal Salary { get; set; }
    public decimal CommissionPct { get; set; }
    public int ManagerId { get; set; }
    public int DepartmentId { get; set; }

    public EmployeeEntity MapToResult()
    {
        return new EmployeeEntity(EmployeeId, FirstName, LastName, Email, Phone, HireDate,JobId,Salary, CommissionPct, ManagerId, DepartmentId);
    }
}