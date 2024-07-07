using Poc.Domain.Entities.Department.Events;
public class DepartmentUpdatedEvent : DepartmentBaseEvent
{
    public DepartmentUpdatedEvent(decimal id, string departmentName, decimal managerId, decimal location)
        : base(id, departmentName, managerId, location)
    {
    }
}