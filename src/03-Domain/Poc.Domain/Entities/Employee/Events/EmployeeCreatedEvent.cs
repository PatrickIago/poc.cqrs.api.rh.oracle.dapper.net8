using Poc.Domain.Entities.Departament;

namespace Poc.Domain.Entities.Employee.Events;
public class EmployeeCreatedEvent : EmployeeBaseEvent
{
    public EmployeeCreatedEvent(int id, string name, string email, string phoneNumber, DateTime hireDate, int jobId, decimal salary, int? managerId, DepartamentEntity department) 
        : base(id, name, email, phoneNumber, hireDate, jobId, salary, managerId, department)
    {
    }
}
