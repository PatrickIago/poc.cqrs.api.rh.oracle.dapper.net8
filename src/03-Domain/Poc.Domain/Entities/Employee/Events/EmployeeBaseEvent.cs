using poc.core.api.net8.Events;
namespace Poc.Domain.Entities.Employee.Events;

public abstract class EmployeeBaseEvent : Event
{
    protected EmployeeBaseEvent(decimal employeeId, string firstName, string lastName, string email, string phone, DateTime hireDate, string jobId, decimal salary, decimal commissionPct, int managerId, int departmentId)
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
    }

    public decimal EmployeeId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public DateTime HireDate { get; private set; }
    public string JobId { get; private set; }
    public decimal Salary { get; private set; }
    public decimal CommissionPct { get; private set; }
    public int ManagerId { get; private set; }
    public int DepartmentId { get; private set; }
}