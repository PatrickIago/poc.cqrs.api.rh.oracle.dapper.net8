using Poc.Domain.Entities.Employee.Events;
public class EmployeeUpdatedEvent : EmployeeBaseEvent
{
    public EmployeeUpdatedEvent(decimal employeeId, string firstName, string lastName, string email, string phone, DateTime hireDate, string jobId, decimal salary, decimal commissionPct, int managerId, int departmentId)
        : base(employeeId, firstName, lastName, email, phone, hireDate, jobId, salary, commissionPct, managerId, departmentId)
    {
    }
}