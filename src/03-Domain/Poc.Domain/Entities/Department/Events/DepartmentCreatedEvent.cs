namespace Poc.Domain.Entities.Department.Events;
public class DepartmentCreatedEvent : DepartmentBaseEvent
{
    public DepartmentCreatedEvent(decimal id, string departmentName,decimal managerId, decimal location)
        : base(id, departmentName, managerId, location)
    {
    }
}