using poc.core.api.net8.Events;
using Poc.Domain.Entities.Departament;
namespace Poc.Domain.Entities.Employee.Events;
public abstract class EmployeeBaseEvent : Event
{
    protected EmployeeBaseEvent(int id, string name, string email, string phoneNumber, DateTime hireDate, int jobId, decimal salary, int? managerId, DepartmentEntity department)
    {
        Id = id;
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        HireDate = hireDate;
        JobId = jobId;
        Salary = salary;
        ManagerId = managerId;
        Departament = department;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public DateTime HireDate { get; private set; }
    public int JobId { get; private set; }
    public decimal Salary { get; private set; }
    public int? ManagerId { get; private set; }
    public DepartmentEntity Departament  { get; private set; }
}
