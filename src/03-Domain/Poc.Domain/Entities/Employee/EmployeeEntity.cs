using poc.core.api.net8;
using poc.core.api.net8.api.net8.api.net8.Abstractions;

namespace Poc.Domain.Entities.Employee;

public class EmployeeEntity : BaseEOraclentity, IAggregateRoot
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

    public EmployeeEntity()
    {
    }

    public EmployeeEntity(string firstName, string lastName, string email, string phone, DateTime hireDate, string jobId, decimal salary, decimal commissionPct, int managerId, int departmentId)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        HireDate = hireDate;
        JobId = jobId;
        Salary = salary;
        CommissionPct = commissionPct;
        ManagerId = managerId;
        DepartmentId = departmentId;
        AddDomainEvent(new EmployeeCreatedEvent(EmployeeId, FirstName, LastName, Email, Phone, HireDate, JobId, Salary, CommissionPct, ManagerId, DepartmentId));
    }

    public EmployeeEntity(decimal employeeId, string firstName, string lastName, string email, string phone, DateTime hireDate, string jobId, decimal salary, decimal commissionPct, int managerId, int departmentId)
    {
        EmployeeId = employeeId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        HireDate = hireDate;
        JobId = jobId;
        Salary = salary;
        CommissionPct = commissionPct;
        ManagerId = managerId;
        DepartmentId = departmentId;
        AddDomainEvent(new EmployeeUpdatedEvent(EmployeeId, FirstName, LastName, Email, Phone, HireDate, JobId, Salary, CommissionPct, ManagerId, DepartmentId));
    }

    public EmployeeEntity(decimal employeeId)
    {
        EmployeeId = employeeId;
        AddDomainEvent(new EmployeeDeletedEvent(EmployeeId, FirstName, LastName, Email, Phone, HireDate, JobId, Salary, CommissionPct, ManagerId, DepartmentId));
    }

    // Este método permitirá definir o RegionId após a entidade ser criada.
    public void SetEmployeeId(decimal employeeId)
    {
        if (EmployeeId == default)
        {
            EmployeeId = employeeId;
        }
        else
        {
            throw new InvalidOperationException("O ID só pode ser definido uma vez.");
        }
    }
}
